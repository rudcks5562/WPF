   M S B A M L   `   `   `  ����M  IFleaMarketV2Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:FleaMarketV2Client.ViewsFleaMarketV2Client.Views  5      +    %FleaMarketV2Client.Views.MySearchView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:FleaMarketV2Client.Views     5      .��5	      �    BackGroundBrush2#��[  6   $��5.78cm��6:   $��10.32cm��6L     � RowDefinitions  5
   
   �� 5      $��0.07* �6   �� 5      $��0.93* �6   .+�5   
   � $��0��6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $
��auto��62   $
��auto��6?   $��5,5,5,0q�6M   .+�5      �� $
��Left=�6   $��	10,0,10,0q�67   $��SEARCH��6J   $
��Wrap=�6X   $��Center=�6l   
�������6�   �� 5      -   5         ��Name$ tb_searchbox��5      $��0��60   $��1��6     BackGroundBrush1#��[ 6�   $	��150��6�   $��0,0,10,0q�6�   
���������6>   �� *SearchQuery+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5      $	��...��6   $��Center=�6'     
BaseButton#��[ 6B   $��38��6f   $��18��6q   5      5      �� 5   
     ListViewItemStyle#��[ 6   $��1��6K     SearchListViewStyle#��[ 6�   $��10,10,10,10q�6�    �� SelectedItem 6X   �� *SelectedCategory+$ PropertyChanged=���6�   �� *
ResultList+��5      �� 5      .R�5      �� $ 
Horizontal=�6!   .+�5      ��   SearchListViewHolderText#��[ 6B   ��6$   �� *CategoryName+6B   �� 5      #��[ 6;   ��6$   �� *Count+6;   5      5       5"      �� 5#   
   $
��Left=�6   $��	10,26,0,0q�6/   $
��Wrap=�6B   $	��Top=�6V   $��11��6n   $��8(�6z   $��2��6�   �� 6�    � Language$ ko-kr�6�   $�Result��6�     BackGroundBrush8#��[ 6�   6�   5%      5&      