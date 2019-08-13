using System;
using iotc_xamarin_ble.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ViewCell), typeof(CustomViewCellRenderer))]
namespace iotc_xamarin_ble.iOS.Renderers
{
    public class CustomViewCellRenderer:ViewCellRenderer
    {
        public override UIKit.UITableViewCell GetCell(Cell item, UIKit.UITableViewCell reusableCell, UIKit.UITableView tv)        {            var cell = base.GetCell(item, reusableCell, tv);            if (cell != null)                cell.SelectionStyle = UIKit.UITableViewCellSelectionStyle.None;            return cell;        }
    }
}
