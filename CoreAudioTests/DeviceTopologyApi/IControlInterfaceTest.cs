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
using System.Runtime.InteropServices;

namespace CoreAudioTests.DeviceTopologyApi
{
    /// <summary>
    /// Tests all methods of the IControlInterface interface.
    /// </summary>
    [TestClass]
    public class IControlInterfaceTest
    {
        /// <summary>
        /// Tests that the ID can be received, for each available control interface in the system.
        /// </summary>
        [TestMethod]
        public void IControlInterface_GetIID()
        {
            int result = 0;
            var allParts = TestUtilities.CreateIPartCollection();

            try
            {
                foreach (var part in allParts)
                {
                    UInt32 count;
                    part.GetControlInterfaceCount(out count);

                    for (uint i = 0; i < count; i++)
                    {
                        IControlInterface ctrl;
                        part.GetControlInterface(i, out ctrl);

                        Guid iid = Guid.Empty;
                        result = ctrl.GetIID(out iid);

                        Marshal.FinalReleaseComObject(ctrl);

                        AssertCoreAudio.IsHResultOk(result);
                        Assert.AreNotEqual(Guid.Empty, iid, "The control IID was not received.");
                    }
                }
            }
            finally
            {
                foreach (var part in allParts)
                    Marshal.FinalReleaseComObject(part);
            }
        }

        /// <summary>
        /// Tests that the name can be received, for each available control interface in the system.
        /// </summary>
        [TestMethod]
        public void IControlInterface_GetName()
        {
            int result = 0;
            var allParts = TestUtilities.CreateIPartCollection();

            try
            {
                foreach (var part in allParts)
                {
                    UInt32 count;
                    part.GetControlInterfaceCount(out count);

                    for (uint i = 0; i < count; i++)
                    {
                        IControlInterface ctrl;
                        part.GetControlInterface(i, out ctrl);

                        string name = "abc123";
                        result = ctrl.GetName(out name);

                        Marshal.FinalReleaseComObject(ctrl);

                        AssertCoreAudio.IsHResultOk(result);
                        Assert.AreNotEqual("abc123", name, "The control name was not received.");
                    }
                }
            }
            finally
            {
                foreach (var part in allParts)
                    Marshal.FinalReleaseComObject(part);
            }
        }
    }
}