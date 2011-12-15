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
using CoreAudioTests.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vannatech.CoreAudio.Interfaces;
using Vannatech.CoreAudio.Constants;
using System.Linq;

namespace CoreAudioTests.DeviceTopologyApi
{
    /// <summary>
    /// Tests all methods of the IAudioBass interface.
    /// </summary>
    [TestClass]
    public class IAudioBassTest
    {
        /// <summary>
        /// Tests that the channel count can be received, for each applicable part in the system.
        /// </summary>
        [TestMethod]
        public void IAudioBass_GetChannelCount()
        {
            (new IPerChannelDbLevelTest<IAudioBass>()).ExecuteGetChannelCountTest();
        }

        /// <summary>
        /// Tests that the level of each channel can be received, for each applicable part in the system.
        /// </summary>
        [TestMethod]
        public void IAudioBass_GetLevel()
        {
            (new IPerChannelDbLevelTest<IAudioBass>()).ExecuteGetLevelTest();
        }

        /// <summary>
        /// Tests that the level range of each channel can be received, for each applicable part in the system.
        /// </summary>
        [TestMethod]
        public void IAudioBass_GetLevelRange()
        {
            (new IPerChannelDbLevelTest<IAudioBass>()).ExecuteGetLevelRangeTest();
        }

        /// <summary>
        /// Tests that the level of each channel can be set, for each applicable part in the system.
        /// </summary>
        [TestMethod]
        public void IAudioBass_SetLevel()
        {
            (new IPerChannelDbLevelTest<IAudioBass>()).ExecuteSetLevelTest();
        }

        /// <summary>
        /// Tests that the level of all channels can be set, for each applicable part in the system.
        /// </summary>
        [TestMethod]
        public void IAudioBass_SetLevelAllChannels()
        {
            (new IPerChannelDbLevelTest<IAudioBass>()).ExecuteSetLevelAllChannelsTest();
        }

        /// <summary>
        /// Tests that the level of all channels can be set uniformly, for each applicable part in the system.
        /// </summary>
        [TestMethod]
        public void IAudioBass_SetLevelUniform()
        {
            (new IPerChannelDbLevelTest<IAudioBass>()).ExecuteSetLevelUniformTest();
        }
    }
}