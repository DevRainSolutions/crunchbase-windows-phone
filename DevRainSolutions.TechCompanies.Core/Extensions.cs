using System;
using System.Linq;
using Microsoft.Phone.Controls;

namespace DevRainSolutions.TechCompanies.Core
{
    public static class PivotItemExtension
    {
        public static bool RemovePivotItemByHeaderName(this Pivot p, string headerKey)
        {
            var item = FindPivotItemWithHeaderName(p, headerKey);
            return item != null && p.Items.Remove(item);
        }

        public static bool RemovePivotItemByName(this Pivot p, string name)
        {
            var item = p.Items.Single(pi => ((PivotItem)pi).Name == name);
            return item != null && p.Items.Remove(item);
        }

        public static PivotItem FindPivotItemWithHeaderName(this Pivot p, string name)
        {
            return (PivotItem)p.Items.FirstOrDefault(mem => name.Equals((string)(((PivotItem)mem).Header), StringComparison.OrdinalIgnoreCase));
        }
    }
}
