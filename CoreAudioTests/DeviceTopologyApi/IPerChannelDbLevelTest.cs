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

namespace CoreAudioTests.DeviceTopologyApi
{
    /// <summary>
    /// Provides test methods for each interface that derives from IPerChannelDbLevel
    /// </summary>
    public class IPerChannelDbLevelTest<T> : TestClass<T> where T : IPerChannelDbLevel
    {
        // No methods are called directly against this interface.
        // It only serves as a base for the following:
            // * IAudioBass
            // * IAudioMidrange
            // * IAudioTreble
            // * IAudioVolumeLevel

        // Methods below server as common tests for each of the derived interfaces.

        internal void ExecuteGetChannelCountTest()
        {
            ExecutePartActivationTest(activation =>
            {
                var count = UInt32.MaxValue;
                var result = activation.GetChannelCount(out count);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(UInt32.MaxValue, count, "The channel count was not received.");
            });
        }

        internal void ExecuteGetLevelTest()
        {
            var tested = false;

            ExecutePartActivationTest(activation =>
            {
                var count = UInt32.MaxValue;
                activation.GetChannelCount(out count);

                for (uint i = 0; i < count; i++)
                {
                    var level = 123.456f;
                    var result = activation.GetLevel(i, out level);

                    AssertCoreAudio.IsHResultOk(result);
                    Assert.AreNotEqual(123.456f, level, "The level was not received.");

                    tested = true;
                }
            });

            if (!tested) Assert.Inconclusive("No channels were available to test against.");
        }

        internal void ExecuteGetLevelRangeTest()
        {
            var tested = false;

            ExecutePartActivationTest(activation =>
            {
                var count = UInt32.MaxValue;
                activation.GetChannelCount(out count);

                for (uint i = 0; i < count; i++)
                {
                    float volMin = 123.456f, volMax = 123.456f, volStep = 123.456f;
                    var result = activation.GetLevelRange(i, out volMin, out volMax, out volStep);

                    AssertCoreAudio.IsHResultOk(result);
                    Assert.AreNotEqual(123.456f, volMin, "The minimum volume was not received.");
                    Assert.AreNotEqual(123.456f, volMax, "The maximum volume was not received.");
                    Assert.AreNotEqual(123.456f, volStep, "The volume increment was not received.");
                    tested = true;
                }
            });

            if (!tested) Assert.Inconclusive("No channels were available to test against.");
        }

        internal void ExecuteSetLevelTest()
        {
            var tested = false;

            ExecutePartActivationTest(activation =>
            {
                var count = UInt32.MaxValue;
                activation.GetChannelCount(out count);

                for (uint i = 0; i < count; i++)
                {
                    float levelOrig, levelNew;
                    activation.GetLevel(i, out levelOrig);

                    float volMin, volMax, volStep;
                    var result = activation.GetLevelRange(i, out volMin, out volMax, out volStep);

                    var context = Guid.NewGuid();
                    result = activation.SetLevel(i, volMin + volStep, context);
                    activation.GetLevel(i, out levelNew);

                    AssertCoreAudio.IsHResultOk(result);
                    Assert.AreEqual(volMin + volStep, levelNew, volStep, "The channel volume was not set properly.");

                    result = activation.SetLevel(i, volMax - volStep, context);
                    activation.GetLevel(i, out levelNew);

                    AssertCoreAudio.IsHResultOk(result);
                    Assert.AreEqual(volMax - volStep, levelNew, volStep, "The channel volume was not set properly.");

                    activation.SetLevel(i, levelOrig, context);
                    tested = true;
                }
            });

            if (!tested) Assert.Inconclusive("No channels were available to test against.");
        }

        internal void ExecuteSetLevelAllChannelsTest()
        {
            var tested = false;

            ExecutePartActivationTest(activation =>
            {
                var count = UInt32.MaxValue;
                activation.GetChannelCount(out count);

                var levelsOrig = new float[count];
                var levelsLow = new float[count];
                var levelsHigh = new float[count];
                float volStep = 0f;

                for (uint i = 0; i < count; i++)
                {
                    activation.GetLevel(i, out levelsOrig[i]);

                    float volMin, volMax;
                    activation.GetLevelRange(i, out volMin, out volMax, out volStep);

                    levelsLow[i] = volMin + volStep;
                    levelsHigh[i] = volMax - volStep;
                }

                var context = Guid.NewGuid();
                var result = activation.SetLevelAllChannels(levelsLow, count, context);
                AssertCoreAudio.IsHResultOk(result);

                for (uint i = 0; i < count; i++)
                {
                    float levelNew;
                    activation.GetLevel(i, out levelNew);
                    Assert.AreEqual(levelsLow[i], levelNew, volStep, "The channel volume was not set properly.");
                }

                result = activation.SetLevelAllChannels(levelsHigh, count, context);
                AssertCoreAudio.IsHResultOk(result);

                for (uint i = 0; i < count; i++)
                {
                    float levelNew;
                    activation.GetLevel(i, out levelNew);
                    Assert.AreEqual(levelsHigh[i], levelNew, volStep, "The channel volume was not set properly.");
                }

                // return to original levels
                for (uint i = 0; i < count; i++)
                {
                    activation.SetLevel(i, levelsOrig[i], context);
                    tested = true;
                }
            });

            if (!tested) Assert.Inconclusive("No channels were available to test against.");
        }

        internal void ExecuteSetLevelUniformTest()
        {
            var tested = false;

            ExecutePartActivationTest(activation =>
            {
                UInt32 count;
                activation.GetChannelCount(out count);

                var levelsOrig = new float[count];
                float volLow = float.MinValue, volHigh = float.MaxValue, volStep = 0f;

                // Get original values.
                for (uint i = 0; i < count; i++)
                {
                    activation.GetLevel(i, out levelsOrig[i]);

                    float volMin, volMax;
                    activation.GetLevelRange(i, out volMin, out volMax, out volStep);

                    if (volMin > volLow)
                        volLow = volMin;

                    if (volMax < volHigh)
                        volHigh = volMax;
                }

                volLow += volStep;
                volHigh -= volStep;

                if (volLow > volHigh) Assert.Inconclusive("The minimum volume is less than the maximum. Thist will not result in a valid test.");

                // Verify setting low value.
                var context = Guid.NewGuid();
                var result = activation.SetLevelUniform(volLow, context);
                AssertCoreAudio.IsHResultOk(result);

                for (uint i = 0; i < count; i++)
                {
                    float levelNew;
                    activation.GetLevel(i, out levelNew);
                    Assert.AreEqual(volLow, levelNew, volStep, "The channel volume was not set properly.");
                }

                // Verify setting high value.
                result = activation.SetLevelUniform(volHigh, context);
                AssertCoreAudio.IsHResultOk(result);

                for (uint i = 0; i < count; i++)
                {
                    float levelNew;
                    activation.GetLevel(i, out levelNew);
                    Assert.AreEqual(volHigh, levelNew, volStep, "The channel volume was not set properly.");
                }

                // return to original levels
                for (uint i = 0; i < count; i++)
                {
                    activation.SetLevel(i, levelsOrig[i], context);
                    tested = true;
                }
            });

            if (!tested) Assert.Inconclusive("No channels were available to test against.");
        }
    }
}