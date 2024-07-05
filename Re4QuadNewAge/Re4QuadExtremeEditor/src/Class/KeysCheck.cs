using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Re4QuadExtremeEditor.src.Class
{
    /// <summary>
    /// usado na verificação de entrada de digitação no propertyGrid
    /// </summary>
    public static class KeysCheck
    {
        public static bool KeyIsNum(Keys key)
        {
            return
            (key == Keys.D0 ||
             key == Keys.D1 ||
             key == Keys.D2 ||
             key == Keys.D3 ||
             key == Keys.D4 ||
             key == Keys.D5 ||
             key == Keys.D6 ||
             key == Keys.D7 ||
             key == Keys.D8 ||
             key == Keys.D9 ||
             key == Keys.NumPad0 ||
             key == Keys.NumPad1 ||
             key == Keys.NumPad2 ||
             key == Keys.NumPad3 ||
             key == Keys.NumPad4 ||
             key == Keys.NumPad5 ||
             key == Keys.NumPad6 ||
             key == Keys.NumPad7 ||
             key == Keys.NumPad8 ||
             key == Keys.NumPad9);
        }
        public static bool KeyIsMinus(Keys key)
        {
            return (key == Keys.Subtract || key == Keys.OemMinus);
        }
        public static bool KeyIsHex(Keys key)
        {
            return (key == Keys.A || key == Keys.B || key == Keys.C || key == Keys.D || key == Keys.E || key == Keys.F);
        }
        public static bool KeyIsEssential(Keys key) 
        {
            return
            (key == Keys.Enter ||
             key == Keys.Escape ||
             key == Keys.Tab ||
             key == Keys.Back ||
             key == Keys.Right ||
             key == Keys.Left ||
             key == Keys.Up ||
             key == Keys.Down ||
             //key == Keys.Control ||
             //key == Keys.ControlKey ||
             //key == Keys.LControlKey ||
             //key == Keys.RControlKey ||
             //key == Keys.Shift ||
             //key == Keys.ShiftKey ||
             //key == Keys.LShiftKey ||
             //key == Keys.RShiftKey ||
             //key == Keys.LMenu ||
             //key == Keys.RMenu ||
             //key == Keys.Alt ||
             key == Keys.Cancel ||
             key == Keys.CapsLock ||
             key == Keys.Clear ||
             key == Keys.Delete ||
             key == Keys.End ||
             key == Keys.Home ||
             key == Keys.PageDown ||
             key == Keys.PageUp ||
             key == Keys.Help ||
             key == Keys.Insert ||
             key == Keys.LButton ||
             key == Keys.RButton ||
             key == Keys.MButton ||
             key == Keys.LWin ||
             key == Keys.RWin ||
             key == Keys.Return ||
             key == Keys.F1 ||
             key == Keys.F2 ||
             key == Keys.F3 ||
             key == Keys.F4 ||
             key == Keys.F5 ||
             key == Keys.F6 ||
             key == Keys.F7 ||
             key == Keys.F8 ||
             key == Keys.F9 ||
             key == Keys.F10||
             key == Keys.F11||
             key == Keys.F12);
        }
        public static bool KeyIsCommaDot(Keys key) 
        {
            return (key == Keys.OemPeriod || key == Keys.Oemcomma || key == Keys.Decimal);
        }
        public static bool KeyIsOnlyDot(int KeyValue)
        {        
            if (KeyValue == 194)
            {
                return true;
            }
            return false;
        }

        public static bool KeyIsEssentialNoKey(Keys key)
        {
            return
            (key == Keys.Enter ||
             key == Keys.Escape ||
             key == Keys.Tab ||
             //key == Keys.Back ||
             key == Keys.Right ||
             key == Keys.Left ||
             key == Keys.Up ||
             key == Keys.Down ||
             //key == Keys.Control ||
             //key == Keys.ControlKey ||
             //key == Keys.LControlKey ||
             //key == Keys.RControlKey ||
             //key == Keys.Shift ||
             //key == Keys.ShiftKey ||
             //key == Keys.LShiftKey ||
             //key == Keys.RShiftKey ||
             //key == Keys.LMenu ||
             //key == Keys.RMenu ||
             //key == Keys.Alt ||
             key == Keys.Cancel ||
             key == Keys.CapsLock ||
             //key == Keys.Clear ||
             //key == Keys.Delete ||
             key == Keys.End ||
             key == Keys.Home ||
             key == Keys.PageDown ||
             key == Keys.PageUp ||
             key == Keys.Help ||
             key == Keys.Insert ||
             key == Keys.LButton ||
             key == Keys.RButton ||
             key == Keys.MButton ||
             key == Keys.LWin ||
             key == Keys.RWin ||
             key == Keys.Return ||
             key == Keys.F1 ||
             key == Keys.F2 ||
             key == Keys.F3 ||
             key == Keys.F4 ||
             key == Keys.F5 ||
             key == Keys.F6 ||
             key == Keys.F7 ||
             key == Keys.F8 ||
             key == Keys.F9 ||
             key == Keys.F10 ||
             key == Keys.F11 ||
             key == Keys.F12);
        }
    }
}
