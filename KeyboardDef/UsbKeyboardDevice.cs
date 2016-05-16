using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KeyboardDef
{
    class UsbKeyboardDevice : HIDDevice
    {
        #region Public attributes/methods
        /// <summary>
        /// Event fired when one or more button state changes
        /// </summary>
        public void WriteConfig()
        {
            //BuzzOutputReport oRep = new BuzzOutputReport(this);	// create output report
            //oRep.SetLightStates(bLight1, bLight2, bLight3, bLight4);	// set the lights states
            try
            {
                //Write(oRep); // write the output report
            }
            catch (HIDDeviceException)
            {
                // Device may have been removed!
            }
        }
        /// <summary>
        /// Finds the LeeKu keyboard. 
        /// </summary>
        /// <returns>A new BuzzHandsetDevice or null if not found.</returns>
        public static UsbKeyboardDevice FindLeeKuKeyboard()
        {
            // VID and PID for Buzz device are 0x054c and 0x1000 respectively
            return (UsbKeyboardDevice)FindDevice(0x20A0, 0x4247, typeof(UsbKeyboardDevice));
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Fired when data has been received over USB
        /// </summary>
        /// <param name="oInRep">Input report received</param>
        protected override void HandleDataReceived(InputReport oInRep)
        {
            // Fire the event handler if assigned
            //if (OnButtonChanged != null)
            //{
                //BuzzInputReport oBuzIn = (BuzzInputReport)oInRep;
                //OnButtonChanged(this, new BuzzButtonChangedEventArgs(oBuzIn.Buttons));
            //}
        }
        /// <summary>
        /// Dispose.
        /// </summary>
        /// <param name="bDisposing">True if object is being disposed - else is being finalised</param>
        protected override void Dispose(bool bDisposing)
        {
            if (bDisposing)
            {
                // before we go, turn all lights off
                //SetLights(false, false, false, false);
            }
            base.Dispose(bDisposing);
        }
        #endregion
    }
}
