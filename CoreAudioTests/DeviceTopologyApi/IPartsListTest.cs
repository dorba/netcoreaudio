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
    /// Tests all methods of the IPartsList interface.
    /// </summary>
    [TestClass]
    public class IPartsListTest
    {
        /// <summary>
        /// Tests that the count of a parts list can be received, for each available parts list in the system.
        /// </summary>
        [TestMethod]
        public void IPartsList_GetCount()
        {
            var result = 0;
            var tested = false;
            var allParts = TestUtilities.CreateIPartCollection();

            try
            {
                // Try using incoming parts.
                foreach (var part in allParts)
                {
                    IPartsList partsList;
                    var enumResult = part.EnumPartsIncoming(out partsList);

                    if ((uint)enumResult == TestUtilities.HRESULTS.E_NOTFOUND)
                        continue;

                    var count = UInt32.MaxValue;
                    result = partsList.GetCount(out count);
                    Marshal.FinalReleaseComObject(partsList);

                    AssertCoreAudio.IsHResultOk(result);
                    Assert.AreNotEqual(UInt32.MaxValue, count, "The count was not received.");
                    tested = true;
                }

                // Try using outgoing parts.
                foreach (var part in allParts)
                {
                    IPartsList partsList;
                    var enumResult = part.EnumPartsOutgoing(out partsList);

                    if ((uint)enumResult == TestUtilities.HRESULTS.E_NOTFOUND)
                        continue;

                    var count = UInt32.MaxValue;
                    result = partsList.GetCount(out count);
                    Marshal.FinalReleaseComObject(partsList);

                    AssertCoreAudio.IsHResultOk(result);
                    Assert.AreNotEqual(UInt32.MaxValue, count, "The count was not received.");
                    tested = true;
                }
            }
            finally
            {
                foreach (var part in allParts)
                    Marshal.FinalReleaseComObject(part);
            }

            if (!tested) Assert.Inconclusive("The test cannot be run properly. No parts lists were found.");
        }

        /// <summary>
        /// Tests that specific parts in a parts list can be received, for each available parts list in the system.
        /// </summary>
        [TestMethod]
        public void IPartsList_GetPart()
        {
            var result = 0;
            var tested = false;
            var allParts = TestUtilities.CreateIPartCollection();

            try
            {
                // Try using incoming parts.
                foreach (var part in allParts)
                {
                    IPartsList partsList;
                    var enumResult = part.EnumPartsIncoming(out partsList);

                    if ((uint)enumResult == TestUtilities.HRESULTS.E_NOTFOUND)
                        continue;

                    UInt32 count;
                    partsList.GetCount(out count);

                    try
                    {
                        for (uint i = 0; i < count; i++)
                        {
                            IPart testPart;
                            result = partsList.GetPart(i, out testPart);
                            Marshal.FinalReleaseComObject(testPart);

                            AssertCoreAudio.IsHResultOk(result);
                            tested = true;
                        }
                    }
                    finally
                    {
                        Marshal.FinalReleaseComObject(partsList);
                    }
                }

                // Try using outgoing parts.
                foreach (var part in allParts)
                {
                    IPartsList partsList;
                    var enumResult = part.EnumPartsOutgoing(out partsList);

                    if ((uint)enumResult == TestUtilities.HRESULTS.E_NOTFOUND)
                        continue;

                    UInt32 count;
                    partsList.GetCount(out count);

                    try
                    {
                        for (uint i = 0; i < count; i++)
                        {
                            IPart testPart;
                            result = partsList.GetPart(i, out testPart);
                            Marshal.FinalReleaseComObject(testPart);

                            AssertCoreAudio.IsHResultOk(result);
                            tested = true;
                        }
                    }
                    finally
                    {
                        Marshal.FinalReleaseComObject(partsList);
                    }
                }
            }
            finally
            {
                foreach (var part in allParts)
                    Marshal.FinalReleaseComObject(part);
            }

            if (!tested) Assert.Inconclusive("The test cannot be run properly. No parts lists were found.");
        }
    }
}