using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class TypeCheck
    {
        private bool tolerantType;
        private bool equivalentType;
        private bool quasiOrdinalType;
        private bool ordinalType;
        private bool strictOrderType;
        private bool linearOrderType;
        private bool strictLinearOrderType;

        public TypeCheck()
        {
            tolerantType = false;
            equivalentType = false;
            quasiOrdinalType = false;
            ordinalType = false;
            strictOrderType = false;
            linearOrderType = false;
            strictLinearOrderType = false;
        }

        private bool tolerantCheck(AttributesCheck attributes)
        {
            if (attributes.IsReflexive && attributes.IsSymmetric) return true;
            return false;
        }

        private bool equivalentCheck(AttributesCheck attributes)
        {
            if (attributes.IsReflexive && attributes.IsSymmetric && attributes.IsTransitive) return true;
            return false;
        }

        private bool quasiOrdinalCheck(AttributesCheck attributes)
        {
            if (attributes.IsReflexive && attributes.IsTransitive) return true;
            return false;
        }

        private bool ordinalCheck(AttributesCheck attributes)
        {
            if (attributes.IsReflexive && attributes.IsAntiSymmetric && attributes.IsTransitive) return true;
            return false;
        }

        private bool strictOrderCheck(AttributesCheck attributes)
        {
            if (attributes.IsAsymmetric && attributes.IsTransitive) return true;
            return false;
        }

        private bool linearOrderCheck(AttributesCheck attributes)
        {
            if (attributes.IsReflexive && attributes.IsAntiSymmetric && attributes.IsTransitive && attributes.IsCoherent) return true;
            return false;
        }

        private bool strictLinearOrderCheck(AttributesCheck attributes)
        {
            if (attributes.IsAntiReflexive && attributes.IsTransitive && attributes.IsAntiSymmetric) return true;
            return false;
        }

        private void checkAll(AttributesCheck attributes)
        {
            tolerantType = tolerantCheck(attributes);
            equivalentType = equivalentCheck(attributes);
            quasiOrdinalType = quasiOrdinalCheck(attributes);
            ordinalType = ordinalCheck(attributes);
            strictOrderType = strictOrderCheck(attributes);
            linearOrderType = linearOrderCheck(attributes);
            strictLinearOrderType = strictLinearOrderCheck(attributes);
        }

        public void ShowType(AttributesCheck attributes)
        {
            checkAll(attributes);

            string tolerantMsg = $"{(tolerantType ? "✔️" : "❌")} - Tolerant\n";
            string equivalentMsg = $"{(equivalentType ? "✔️" : "❌")} - Equivalent\n";
            string quasiMsg = $"{(quasiOrdinalType ? "✔️" : "❌")} - Quasi-ordinal\n";
            string ordinalMsg = $"{(ordinalType ? "✔️" : "❌")} - Ordinal\n";
            string strictOrderMsg = $"{(strictOrderType ? "✔️" : "❌")} - Strict ordered\n";
            string lineOrderMsg = $"{(linearOrderType ? "✔️" : "❌")} - Linear ordered\n";
            string strictLineOrderMsg = $"{(strictLinearOrderType ? "✔️" : "❌")} - Strict linear ordered\n";

            string message = tolerantMsg + equivalentMsg + quasiMsg + ordinalMsg + strictOrderMsg + lineOrderMsg + strictLineOrderMsg;
            MessageBox.Show(message, "Type check");
        }


    }
}
