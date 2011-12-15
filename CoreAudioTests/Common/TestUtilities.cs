// -----------------------------------------
// SoundScribe (TM) and related software.
// 
// Copyright (C) 2007-2011 Vannatech
// http://www.vannatech.com
// All rights reserved.
// 
// This source code is subject to the MIT License.
// http://www.opensource.org/licenses/mit-license.php
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// -----------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vannatech.CoreAudio.Constants;
using Vannatech.CoreAudio.Enumerations;
using Vannatech.CoreAudio.Externals;
using Vannatech.CoreAudio.Interfaces;

namespace CoreAudioTests.Common
{
    /// <summary>
    /// Provides common methods and classes for testing CoreAudio API.
    /// </summary>
    internal class TestUtilities
    {
        /// <summary>
        /// Contains various HRESULT codes of interest for testing.
        /// </summary>
        public static class HRESULTS
        {
            /// <summary>
            /// HRESULT returned for element not found error.
            /// </summary>
            public const UInt32 E_NOTFOUND = 0x80070490;

            /// <summary>
            /// HRESULT returned when an object does not support the specified interface.
            /// </summary>
            public const UInt32 E_NOINTERFACE = 0x80004002;

            /// <summary>
            /// HRESULT returned when an arguemnt is invalid for the object's current state.
            /// </summary>
            public const UInt32 E_INVALIDARG = 0x80070057;
        }

        /// <summary>
        /// Checks to see if an HRESULT code is a WASAPI error code.
        /// </summary>
        /// <param name="hResult">The HRESULT code.</param>
        public static bool IsWasapiError(int hResult)
        {
            return ((((uint)hResult) > 0x88890000) && (((uint)hResult) < 0x88890018));
        }

        /// <summary>
        /// Creates a usable IMMDevice interface or the specified direction, using the IMMDeviceEnumerator.GetDefaultAudioEndpoint method.
        /// </summary>
        public static IMMDevice CreateIMMDevice(EDataFlow direction)
        {
            var deviceEnumerator = CreateIMMDeviceEnumerator();

            IMMDevice deviceOut;
            deviceEnumerator.GetDefaultAudioEndpoint(direction, ERole.eMultimedia, out deviceOut);

            return deviceOut;
        }

        /// <summary>
        /// Creates a usable collection of the IMMDevice interfaces.
        /// </summary>
        public static IEnumerable<IMMDevice> CreateIMMDeviceCollection()
        {
            return CreateIMMDeviceCollection(EDataFlow.eAll, DEVICE_STATE_XXX.DEVICE_STATEMASK_ALL);
        }

        /// <summary>
        /// Creates the specified collection of the IMMDevice interfaces.
        /// </summary>
        /// <param name="direction">The data flow direction.</param>
        /// <param name="stateMasks">The state masks.</param>
        public static IEnumerable<IMMDevice> CreateIMMDeviceCollection(EDataFlow direction, UInt32 stateMasks)
        {
            var deviceEnumerator = CreateIMMDeviceEnumerator();

            IMMDeviceCollection deviceCollection;
            deviceEnumerator.EnumAudioEndpoints(direction, stateMasks, out deviceCollection);

            UInt32 deviceCount;
            deviceCollection.GetCount(out deviceCount);

            var deviceList = new List<IMMDevice>();

            for (uint i = 0; i < deviceCount; i++)
            {
                IMMDevice device;
                deviceCollection.Item(i, out device);
                deviceList.Add(device);
            }

            if (!deviceList.Any()) Assert.Inconclusive("The test cannot be run properly. No devices were found.");

            return deviceList;
        }

        /// <summary>
        /// Creats a usable IMMDeviceEnumerator interface.
        /// </summary>
        public static IMMDeviceEnumerator CreateIMMDeviceEnumerator()
        {
            var deviceEnumeratorType = Type.GetTypeFromCLSID(new Guid(ComCLSIDs.MMDeviceEnumeratorCLSID));
            return (IMMDeviceEnumerator)Activator.CreateInstance(deviceEnumeratorType);
        }

        /// <summary>
        /// Creates a collection of activated MMDevices for the specified interface.
        /// </summary>
        /// <typeparam name="T">The type of the interface.</typeparam>
        /// <param name="comIId">The COM interface ID.</param>
        /// <returns>A collection of DeviceActivation objects using the specified interface.</returns>
        public static IEnumerable<DeviceActivation<T>> CreateDeviceActivationCollection<T>(string comIId)
        {
            object objInstance;
            var iid = new Guid(comIId);
            var activationList = new List<DeviceActivation<T>>();
            var activeDevices = CreateIMMDeviceCollection(EDataFlow.eAll, DEVICE_STATE_XXX.DEVICE_STATE_ACTIVE);

            foreach (var device in activeDevices)
            {
                var result = device.Activate(iid, (uint)CLSCTX.CLSCTX_INPROC_SERVER, IntPtr.Zero, out objInstance);
                AssertCoreAudio.IsHResultOk(result);

                if (objInstance != null)
                {
                    activationList.Add(new DeviceActivation<T>
                    {
                        MMDevice = device,
                        ActiveInterface = (T)objInstance
                    });
                }
            }

            if (!activationList.Any()) Assert.Inconclusive("The test cannot be run properly. No interface instances were found for the specified type.");

            return activationList;
        }

        /// <summary>
        /// Creates a collection of the given interface via the IMMDevice Activate method.
        /// </summary>
        /// <typeparam name="T">The type of the interface.</typeparam>
        /// <param name="comIId">The COM interface ID.</param>
        /// <returns>A collection of objects implementing the specified interface.</returns>
        public static IEnumerable<T> ActivateFromIMMDevice<T>(string comIId)
        {
            return CreateDeviceActivationCollection<T>(comIId).Select(mmd => mmd.ActiveInterface);
        }

        /// <summary>
        /// Recursively finds all outgoing parts.
        /// </summary>
        /// <param name="part">The first part.</param>
        /// <returns></returns>
        public static List<IPart> FindParts(IPart part)
        {
            List<IPart> allParts = new List<IPart>();
            allParts.Add(part);

            PartType pType;
            part.GetPartType(out pType);

            if (pType == PartType.Connector)
            {
                var connector = (IConnector)part;

                // If the connector is not connected return.
                bool isConnected;
                connector.IsConnected(out isConnected);
                if (!isConnected) return allParts;

                // Otherwise, get the other connector.
                IConnector connectedTo;
                connector.GetConnectedTo(out connectedTo);
                IPart connectedPart = (IPart)connectedTo;

                // Then enumerate all outgoing parts.
                IPartsList partsOutgoing;
                connectedPart.EnumPartsOutgoing(out partsOutgoing);
                if (partsOutgoing == null) return allParts;

                // If there are more outgoing parts, get each one.
                UInt32 partCount;
                partsOutgoing.GetCount(out partCount);

                for (uint i = 0; i < partCount; i++)
                {
                    IPart nextPart;
                    partsOutgoing.GetPart(i, out nextPart);
                    allParts.AddRange(FindParts(nextPart));
                }

            }
            else if (pType == PartType.Subunit)
            {
                // Get sub type, for debugging purposes.
                Guid subType;
                part.GetSubType(out subType);

                // Just enumerate all outgoing parts.
                IPartsList partsOutgoing;
                part.EnumPartsOutgoing(out partsOutgoing);
                if (partsOutgoing == null) return allParts;

                // If there are more outgoing parts, get each one.
                UInt32 partCount;
                partsOutgoing.GetCount(out partCount);

                for (uint i = 0; i < partCount; i++)
                {
                    IPart nextPart;
                    partsOutgoing.GetPart(i, out nextPart);
                    allParts.AddRange(FindParts(nextPart));
                }
            }

            return allParts;
        }

        /// <summary>
        /// Creates a collection of IPart instances, obtaining all parts that can be found on the system.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IPart> CreateIPartCollection()
        {
            var partList = new List<IPart>();
            var topologies = ActivateFromIMMDevice<IDeviceTopology>(ComIIDs.IDeviceTopologyIID);

            foreach (var top in topologies)
            {
                IConnector connector;
                top.GetConnector(0, out connector);

                partList.AddRange(FindParts((IPart)connector));
            }

            if (!partList.Any()) Assert.Inconclusive("The test cannot be run properly. No part interfaces were found.");

            return partList.Distinct(new PartComparer());
        }

        /// <summary>
        /// Creates a collection of activated Parts for the specified interface.
        /// </summary>
        /// <typeparam name="T">The type of the interface.</typeparam>
        /// <param name="comIId">The COM interface ID.</param>
        /// <returns>A collection of PartActivation objects using the specified interface.</returns>
        public static IEnumerable<PartActivation<T>> CreatePartActivationCollection<T>(string comIId)
        {
            object objInstance;
            var iid = new Guid(comIId);
            var activationList = new List<PartActivation<T>>();
            var partList = CreateIPartCollection();

            foreach (var part in partList)
            {
                var result = part.Activate((uint)CLSCTX.CLSCTX_INPROC_SERVER, ref iid, out objInstance);
                if ((uint)result == HRESULTS.E_NOINTERFACE) continue;

                AssertCoreAudio.IsHResultOk(result);
                activationList.Add(new PartActivation<T>
                {
                    Part = part,
                    ActiveInterface = (T)objInstance
                });
            }

            if (!activationList.Any()) Assert.Inconclusive("The test may not have run properly. No interface instances were found for the specified type.");

            return activationList;
        }

        /// <summary>
        /// Creates a collection of the given interface via the IPart Activate method.
        /// </summary>
        /// <typeparam name="T">The type of the interface.</typeparam>
        /// <param name="comIId">The COM interface ID.</param>
        /// <returns>A collection of objects implementing the specified interface.</returns>
        public static IEnumerable<T> ActivateFromIPart<T>(string comIId)
        {
            return CreatePartActivationCollection<T>(comIId).Select(p => p.ActiveInterface);
        }

        /// <summary>
        /// Creates a collection of the given interface via the IAudioClient GetService method, in shared mode.
        /// </summary>
        /// <typeparam name="T">The type of the interface.</typeparam>
        /// <param name="comIId">The COM interface ID.</param>
        /// <param name="exclusiveMode">A value indicating whether or not to use exclusive mode.</param>
        /// <returns>A collection of audio clients with services implementing the specified interface.</returns>
        public static IEnumerable<AudioClientService<T>> CreateAudioClientServiceCollection<T>(string comIId)
        {
            return CreateAudioClientServiceCollection<T>(comIId, false, 0);
        }

        /// <summary>
        /// Creates a collection of the given interface via the IAudioClient GetService method.
        /// </summary>
        /// <typeparam name="T">The type of the interface.</typeparam>
        /// <param name="comIId">The COM interface ID.</param>
        /// <param name="exclusiveMode">A value indicating whether or not to use exclusive mode.</param>
        /// <returns>A collection of audio clients with services implementing the specified interface.</returns>
        public static IEnumerable<AudioClientService<T>> CreateAudioClientServiceCollection<T>(string comIId, bool exclusiveMode)
        {
            return CreateAudioClientServiceCollection<T>(comIId, exclusiveMode, 0);
        }

        /// <summary>
        /// Creates a collection of the given interface via the IAudioClient GetService method.
        /// </summary>
        /// <typeparam name="T">The type of the interface.</typeparam>
        /// <param name="comIId">The COM interface ID.</param>
        /// <param name="exclusiveMode">A value indicating whether or not to use exclusive mode.</param>
        /// <param name="streamFlags">The stream initialization flags.</param>
        /// <returns>A collection of audio clients with services implementing the specified interface.</returns>
        public static IEnumerable<AudioClientService<T>> CreateAudioClientServiceCollection<T>(string comIId, bool exclusiveMode, UInt32 streamFlags)
        {
            object objInstance;
            var iid = new Guid(comIId);
            var interfaceList = new List<AudioClientService<T>>();
            var audioClients = ActivateFromIMMDevice<IAudioClient>(ComIIDs.IAudioClientIID);

            foreach (var ac in audioClients)
            {
                var shareMode = exclusiveMode ? AUDCLNT_SHAREMODE.AUDCLNT_SHAREMODE_EXCLUSIVE : AUDCLNT_SHAREMODE.AUDCLNT_SHAREMODE_SHARED;
                var formatPtr = GetFormatPointer(ac, shareMode);
                if (formatPtr == IntPtr.Zero) continue;

                var context = Guid.NewGuid();
                var result = ac.Initialize(shareMode, streamFlags, 5000000, 0, formatPtr, context);
                if (IsWasapiError(result)) continue;
                AssertCoreAudio.IsHResultOk(result);

                result = ac.GetService(iid, out objInstance);
                if ((uint)result == HRESULTS.E_NOINTERFACE) continue;
                if (IsWasapiError(result)) continue;
                AssertCoreAudio.IsHResultOk(result);

                interfaceList.Add(new AudioClientService<T>
                {
                    AudioClient = ac,
                    ServiceInterface = (T)objInstance,
                    EventContext = context,
                    ShareMode = shareMode
                });
            }

            if (!interfaceList.Any()) Assert.Inconclusive("The test may not have run properly. No interface instances were found for the specified type.");

            return interfaceList;
        }

        /// <summary>
        /// Tries to resolve a valid format pointer for the audio client.
        /// </summary>
        /// <param name="audioClient">The audio client to use.</param>
        /// <param name="shareMode">The share mode to use.</param>
        /// <returns>A pointer to a valid format, or zero if one cannot be found.</returns>
        public static IntPtr GetFormatPointer(IAudioClient audioClient, AUDCLNT_SHAREMODE shareMode)
        {
            var formatPtr = IntPtr.Zero;

            if (shareMode == AUDCLNT_SHAREMODE.AUDCLNT_SHAREMODE_SHARED)
            {
                audioClient.GetMixFormat(out formatPtr);
            }
            else
            {
                // Otherwise we need to find a supported format
                foreach(var format in TestWaveFormats)
                {
                    var formatMatch = IntPtr.Zero;
                    var supported = audioClient.IsFormatSupported(shareMode, format, out formatMatch);

                    if (supported == 0) { formatPtr = format; break; }
                    else if (supported == 1) { formatPtr = formatMatch; break; }
                }

                if (formatPtr == IntPtr.Zero)
                    Assert.Inconclusive("Unable to find a valid format pointer.");
            }

            return formatPtr;
        }
        
        /// <summary>
        /// Converts a WAVEFORMATEX pointer to a WAVEFORMATEX structure.
        /// </summary>
        /// <param name="formatPtr">The pointer to the wave format.</param>
        /// <returns>A new wave format structure.</returns>
        public static WAVEFORMATEX PointerToWaveFormat(IntPtr formatPtr)
        {
            WAVEFORMATEX waveFormat = new WAVEFORMATEX();
            if (formatPtr == IntPtr.Zero) return waveFormat;

            return (WAVEFORMATEX)Marshal.PtrToStructure(formatPtr, typeof(WAVEFORMATEX));
        }

        /// <summary>
        /// Gets a collection of wave formats commonly used in pro audio.
        /// </summary>
        public static IEnumerable<IntPtr> TestWaveFormats
        {
            get
            {
                var sampleRates = new int[] { 48000, 96000, 44100, 88200 };

                // Pro audio 24 bit formats
                foreach (var sampleRate in sampleRates)
                {
                    var format = new WAVEFORMATEX
                    {
                        wFormatTag = 0xFFFE,
                        nChannels = 2,
                        nSamplesPerSec = (uint)sampleRate,
                        wBitsPerSample = 24,
                        nBlockAlign = 2 * (24 / 8),
                        nAvgBytesPerSec = (uint)((2 * (24 / 8)) * sampleRate)
                    };

                    var samples = new WAVEFORMATEXTENSIBLE.SamplesUnion();
                    samples.wValidBitsPerSample = 24;

                    var extensible = new WAVEFORMATEXTENSIBLE
                    {
                        Format = format,
                        SubFormat = new Guid("00000001-0000-0010-8000-00aa00389b71"),
                        Samples = samples,
                        dwChannelMask = 0x0003
                    };

                    var outPtr = IntPtr.Zero;
                    Marshal.StructureToPtr(extensible, outPtr, true);
                    yield return outPtr;
                }

                // IEEE float formats
                foreach (var sampleRate in sampleRates)
                {
                    var format = new WAVEFORMATEX
                    {
                        wFormatTag = 0xFFFE,
                        nChannels = 2,
                        nSamplesPerSec = (uint)sampleRate,
                        wBitsPerSample = 32,
                        nBlockAlign = 2 * (32 / 8),
                        nAvgBytesPerSec = (uint)((2 * (32 / 8)) * sampleRate)
                    };

                    var samples = new WAVEFORMATEXTENSIBLE.SamplesUnion();
                    samples.wValidBitsPerSample = 32;

                    var extensible = new WAVEFORMATEXTENSIBLE
                    {
                        Format = format,
                        SubFormat = new Guid("00000003-0000-0010-8000-00aa00389b71"),
                        Samples = samples,
                        dwChannelMask = 0x0003
                    };

                    var outPtr = IntPtr.Zero;
                    Marshal.StructureToPtr(extensible, outPtr, true);
                    yield return outPtr;
                }

                // Pro audio 16 bit formats
                foreach (var sampleRate in sampleRates)
                {
                    var format = new WAVEFORMATEX
                    {
                        wFormatTag = 0xFFFE,
                        nChannels = 2,
                        nSamplesPerSec = (uint)sampleRate,
                        wBitsPerSample = 16,
                        nBlockAlign = 2 * (16 / 8),
                        nAvgBytesPerSec = (uint)((2 * (16 / 8)) * sampleRate)
                    };

                    var samples = new WAVEFORMATEXTENSIBLE.SamplesUnion();
                    samples.wValidBitsPerSample = 16;

                    var extensible = new WAVEFORMATEXTENSIBLE
                    {
                        Format = format,
                        SubFormat = new Guid("00000001-0000-0010-8000-00aa00389b71"),
                        Samples = samples,
                        dwChannelMask = 0x0003
                    };

                    var outPtr = IntPtr.Zero;
                    Marshal.StructureToPtr(extensible, outPtr, true);
                    yield return outPtr;
                }
            }
        }
    }
}
