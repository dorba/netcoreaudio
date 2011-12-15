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
using System.Runtime.InteropServices;
using Vannatech.CoreAudio.Constants;

// This file does not contain any actual interface members.
// It is only intended to abstract away COM specifics, such as IIDs.

namespace Vannatech.CoreAudio.Interfaces
{
	// -------------------------------------------------
	// Begin MMDevice API definitions:
    // Defined in Mmdeviceapi.h
	// -------------------------------------------------

    [Guid(ComIIDs.IMMDeviceIID)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IMMDevice { }

    [Guid(ComIIDs.IMMDeviceEnumeratorIID)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IMMDeviceEnumerator { }

    [Guid(ComIIDs.IMMDeviceCollectionIID)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IMMDeviceCollection { }

    [Guid(ComIIDs.IMMEndpointIID)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IMMEndpoint { }

    [Guid(ComIIDs.IMMNotificationClientIID)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IMMNotificationClient { }


	// -------------------------------------------------
	// Begin WASAPI definitions:
    // Defined in Audioclient.h and Audiopolicy.h
	// -------------------------------------------------

	[Guid(ComIIDs.IAudioCaptureClientIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioCaptureClient { }

	[Guid(ComIIDs.IAudioClientIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioClient { }

	[Guid(ComIIDs.IAudioClockIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioClock { }

	[Guid(ComIIDs.IAudioClock2IID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioClock2 { }

	[Guid(ComIIDs.IAudioClockAdjustmentIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioClockAdjustment { }

	[Guid(ComIIDs.IAudioRenderClientIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioRenderClient { }

	[Guid(ComIIDs.IAudioSessionControlIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioSessionControl { }

	[Guid(ComIIDs.IAudioSessionControl2IID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioSessionControl2 { }

	[Guid(ComIIDs.IAudioSessionEnumeratorIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioSessionEnumerator { }

	[Guid(ComIIDs.IAudioSessionEventsIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioSessionEvents { }

	[Guid(ComIIDs.IAudioSessionManagerIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioSessionManager { }

	[Guid(ComIIDs.IAudioSessionManager2IID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioSessionManager2 { }

	[Guid(ComIIDs.IAudioSessionNotificationIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioSessionNotification { }

	[Guid(ComIIDs.IAudioStreamVolumeIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioStreamVolume { }

	[Guid(ComIIDs.IAudioVolumeDuckNotificationIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioVolumeDuckNotification { }

	[Guid(ComIIDs.IChannelAudioVolumeIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IChannelAudioVolume { }

	[Guid(ComIIDs.ISimpleAudioVolumeIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface ISimpleAudioVolume { }


	// -------------------------------------------------
	// Begin DeviceTopology API definitions:
    // Defined in Devicetopology.h
	// -------------------------------------------------

	[Guid(ComIIDs.IAudioAutoGainControlIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioAutoGainControl { }

	[Guid(ComIIDs.IAudioBassIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioBass { }

	[Guid(ComIIDs.IAudioChannelConfigIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioChannelConfig { }

	[Guid(ComIIDs.IAudioInputSelectorIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioInputSelector { }

	[Guid(ComIIDs.IAudioLoudnessIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioLoudness { }

	[Guid(ComIIDs.IAudioMidrangeIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioMidrange { }

	[Guid(ComIIDs.IAudioMuteIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioMute { }

	[Guid(ComIIDs.IAudioOutputSelectorIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioOutputSelector { }

	[Guid(ComIIDs.IAudioPeakMeterIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioPeakMeter { }

	[Guid(ComIIDs.IAudioTrebleIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioTreble { }

	[Guid(ComIIDs.IAudioVolumeLevelIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IAudioVolumeLevel { }

	[Guid(ComIIDs.IConnectorIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IConnector { }

	[Guid(ComIIDs.IControlChangeNotifyIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IControlChangeNotify { }

	[Guid(ComIIDs.IControlInterfaceIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IControlInterface { }

	[Guid(ComIIDs.IDeviceSpecificPropertyIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IDeviceSpecificProperty { }

	[Guid(ComIIDs.IDeviceTopologyIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IDeviceTopology { }

	[Guid(ComIIDs.IKsFormatSupportIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IKsFormatSupport { }

	[Guid(ComIIDs.IKsJackDescriptionIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IKsJackDescription { }

	[Guid(ComIIDs.IKsJackDescription2IID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IKsJackDescription2 { }

	[Guid(ComIIDs.IKsJackSinkInformationIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IKsJackSinkInformation { }

	[Guid(ComIIDs.IPartIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IPart { }

	[Guid(ComIIDs.IPartsListIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IPartsList { }

	[Guid(ComIIDs.IPerChannelDbLevelIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface IPerChannelDbLevel { }

	[Guid(ComIIDs.ISubunitIID)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public partial interface ISubunit { }


	// -------------------------------------------------
	// Begin EndpointVolume API definitions:
    // Defined in Endpointvolume.h
	// -------------------------------------------------

    [Guid(ComIIDs.IAudioEndpointVolumeIID)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IAudioEndpointVolume { }

	[Guid(ComIIDs.IAudioEndpointVolumeExIID)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IAudioEndpointVolumeEx { }

    [Guid(ComIIDs.IAudioMeterInformationIID)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IAudioMeterInformation { }

    [Guid(ComIIDs.IAudioEndpointVolumeCallbackIID)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IAudioEndpointVolumeCallback { }
}
