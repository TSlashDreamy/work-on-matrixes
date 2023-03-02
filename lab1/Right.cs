using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace lab1
{
    internal class Right
    {
        private string rightText = "©️ Chaika Taras | KNS-11";
        public string RightText { get { return rightText; } }

        private bool safe_to_launch = false;
        public bool Safe_to_launch { set { safe_to_launch = value; } }

        // ---- Functions ----

        /// <summary>
        /// Safely exits app in case the rights was corrupted
        /// </summary>
        public void SafeExit()
        {
            if (!safe_to_launch)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Warns in case the rights was corrupted
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void SafeWarning(Button resultBtn, Label rightLabel)
        {
            if (!safe_to_launch)
            {
                resultBtn.Enabled = false;
                rightLabel.Text = "⚠️ Rights corrupted!";
                throw new Exception("Oh no! It's look's like the rights is corrupted. Please contact with the developer!");
            }
        }

    }
}
