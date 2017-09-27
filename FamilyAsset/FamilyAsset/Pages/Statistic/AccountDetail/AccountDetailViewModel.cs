using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.Statistic.AccountDetail
{
    class AccountDetailViewModel : AccountDetailBase
    {

        //<DataTemplate DataType="local:AccountDetailViewModel">
        //    <StackPanel Orientation="Horizontal" Height="50" Width="350">
        //        <i:Interaction.Triggers>
        //            <i:EventTrigger EventName="MouseLeftButtonDown">
        //                <i:InvokeCommandAction Command="{Binding ItemClicked}"/>
        //            </i:EventTrigger>
        //        </i:Interaction.Triggers>
        //        <Image Height="50" Width="50" Source="{Binding ItemImg}"/>
        //        <TextBlock Style="{StaticResource TextBlockStyleBaseChs}" Text="{Binding ItemName}"/>
        //        <TextBlock Style="{StaticResource TextBlockStyleBaseChs}" Text="{Binding Amount}">
        //            <TextBlock.Foreground>
        //                <SolidColorBrush Color="{Binding AmountColor}"/>
        //            </TextBlock.Foreground>
        //        </TextBlock>
        //    </StackPanel>
        //</DataTemplate>

        private string _itemName;

        public string ItemName
        {
            get { return _itemName; }
            set
            {
                _itemName = value;
                RaisePropertyChanged("ItemName");
            }
        }

        private string _amount;

        public string Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                RaisePropertyChanged("Amount");
            }
        }

        private Color _amountColor;

        public Color AmountColor
        {
            get { return _amountColor; }
            set
            {
                _amountColor = value;
                RaisePropertyChanged("AmountColor");
            }
        }


    }
}
