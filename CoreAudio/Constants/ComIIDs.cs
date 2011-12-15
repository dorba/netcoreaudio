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

// Turn off warning for XML comments
// These constants are self documenting
#pragma warning disable 1591

namespace Vannatech.CoreAudio.Constants
{
    /// <summary>
    /// Defines all COM IIDs applicable to CoreAudio API.
    /// </summary>
    public class ComIIDs
    {
        // MMDevice ComIdentifiers
        public const string IMMDeviceIID = "D666063F-1587-4E43-81F1-B948E807363F";
        public const string IMMDeviceEnumeratorIID = "A95664D2-9614-4F35-A746-DE8DB63617E6";
        public const string IMMDeviceCollectionIID = "0BD7A1BE-7A1A-44DB-8397-CC5392387B5E";
        public const string IMMEndpointIID = "1BE09788-6894-4089-8586-9A2A6C265AC5";
        public const string IMMNotificationClientIID = "7991EEC9-7E89-4D85-8390-6C703CEC60C0";

        // WASAPI
        public const string IAudioCaptureClientIID = "C8ADBD64-E71E-48a0-A4DE-185C395CD317";
        public const string IAudioClientIID = "1CB9AD4C-DBFA-4c32-B178-C2F568A703B2";
        public const string IAudioClockIID = "CD63314F-3FBA-4a1b-812C-EF96358728E7";
        public const string IAudioClock2IID = "6f49ff73-6727-49ac-a008-d98cf5e70048";
        public const string IAudioClockAdjustmentIID = "f6e4c0a0-46d9-4fb8-be21-57a3ef2b626c";
        public const string IAudioRenderClientIID = "F294ACFC-3146-4483-A7BF-ADDCA7C260E2";
        public const string IAudioSessionControlIID = "F4B1A599-7266-4319-A8CA-E70ACB11E8CD";
        public const string IAudioSessionControl2IID = "bfb7ff88-7239-4fc9-8fa2-07c950be9c6d";
        public const string IAudioSessionEnumeratorIID = "E2F5BB11-0570-40CA-ACDD-3AA01277DEE8";
        public const string IAudioSessionEventsIID = "24918ACC-64B3-37C1-8CA9-74A66E9957A8";
        public const string IAudioSessionManagerIID = "BFA971F1-4D5E-40BB-935E-967039BFBEE4";
        public const string IAudioSessionManager2IID = "77AA99A0-1BD6-484F-8BC7-2C654C9A9B6F";
        public const string IAudioSessionNotificationIID = "641DD20B-4D41-49CC-ABA3-174B9477BB08";
        public const string IAudioStreamVolumeIID = "93014887-242D-4068-8A15-CF5E93B90FE3";
        public const string IAudioVolumeDuckNotificationIID = "C3B284D4-6D39-4359-B3CF-B56DDB3BB39C";
        public const string IChannelAudioVolumeIID = "1C158861-B533-4B30-B1CF-E853E51C59B8";
        public const string ISimpleAudioVolumeIID = "87CE5498-68D6-44E5-9215-6DA47EF883D8";

        // DeviceTopology
        public const string IAudioAutoGainControlIID = "85401FD4-6DE4-4b9d-9869-2D6753A82F3C";
        public const string IAudioBassIID = "A2B1A1D9-4DB3-425D-A2B2-BD335CB3E2E5";
        public const string IAudioChannelConfigIID = "BB11C46F-EC28-493C-B88A-5DB88062CE98";
        public const string IAudioInputSelectorIID = "4F03DC02-5E6E-4653-8F72-A030C123D598";
        public const string IAudioLoudnessIID = "7D8B1437-DD53-4350-9C1B-1EE2890BD938";
        public const string IAudioMidrangeIID = "5E54B6D7-B44B-40D9-9A9E-E691D9CE6EDF";
        public const string IAudioMuteIID = "DF45AEEA-B74A-4B6B-AFAD-2366B6AA012E";
        public const string IAudioOutputSelectorIID = "BB515F69-94A7-429e-8B9C-271B3F11A3AB";
        public const string IAudioPeakMeterIID = "DD79923C-0599-45e0-B8B6-C8DF7DB6E796";
        public const string IAudioTrebleIID = "0A717812-694E-4907-B74B-BAFA5CFDCA7B";
        public const string IAudioVolumeLevelIID = "7FB7B48F-531D-44A2-BCB3-5AD5A134B3DC";
        public const string IConnectorIID = "9c2c4058-23f5-41de-877a-df3af236a09e";
        public const string IControlChangeNotifyIID = "A09513ED-C709-4d21-BD7B-5F34C47F3947";
        public const string IControlInterfaceIID = "45d37c3f-5140-444a-ae24-400789f3cbf3";
        public const string IDeviceSpecificPropertyIID = "3B22BCBF-2586-4af0-8583-205D391B807C";
        public const string IDeviceTopologyIID = "2A07407E-6497-4A18-9787-32F79BD0D98F";
        public const string IKsFormatSupportIID = "3CB4A69D-BB6F-4D2B-95B7-452D2C155DB5";
        public const string IKsJackDescriptionIID = "4509F757-2D46-4637-8E62-CE7DB944F57B";
        public const string IKsJackDescription2IID = "478F3A9B-E0C9-4827-9228-6F5505FFE76A";
        public const string IKsJackSinkInformationIID = "D9BD72ED-290F-4581-9FF3-61027A8FE532";
        public const string IPartIID = "AE2DE0E4-5BCA-4F2D-AA46-5D13F8FDB3A9";
        public const string IPartsListIID = "6DAA848C-5EB0-45CC-AEA5-998A2CDA1FFB";
        public const string IPerChannelDbLevelIID = "C2F8E001-F205-4BC9-99BC-C13B1E048CCB";
        public const string ISubunitIID = "82149A85-DBA6-4487-86BB-EA8F7FEFCC71";

        // EndpointVolume
        public const string IAudioEndpointVolumeIID = "5CDF2C82-841E-4546-9722-0CF74078229A";
        public const string IAudioEndpointVolumeExIID = "66E11784-F695-4F28-A505-A7080081A78F";
        public const string IAudioMeterInformationIID = "C02216F6-8C67-4B5B-9D00-D008E73E0064";
        public const string IAudioEndpointVolumeCallbackIID = "657804FA-D6AD-4496-8A60-352752AF4F89";
    }
}

#pragma warning restore 1591