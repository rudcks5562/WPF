   M S B A M L   `   `   `  ����M  IFleaMarketV2Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:FleaMarketV2Client.ViewsFleaMarketV2Client.Views  5   	   )    #FleaMarketV2Client.Views.SignInView   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:FleaMarketV2Client.Views     5   	     � WindowStyle$
  None=�5   
   $��7.82cm��6   $��10.84cm��60   .��5	      �    BackGroundBrush1#��[  6    � RowDefinitions 5   
   �� 5      $��0.08* �6   6-   �� 5      $��* �6   6)   .+�5   
   � .+�5      1   +FleaMarketV2Client.Views.WindowStyleControl     Title$ SignIn��6'   $��1��67   5      � 5   
   $��2��6   .+�5      ��   MainLogoTextStyle#��[ 6   $
��Left=�6D   $��	16,18,0,0q�6_   $
��Wrap=�6r   $	��Top=�6�   $	��240��6�   �� 6�    � Language$ ko-kr�6�   $�SIGN-IN��6�   6�   �� 5        BaseTextBlockStyle#��[ 6   $
��Left=�6D   $��	28,90,0,0q�6_   $��USERNAME��6r   $	��Top=�6�   �� 5      #��[ 6   $
��Left=�6E   $��
28,130,0,0q�6`   $��PASSWORD��6t   $
��Wrap=�6�   $	��Top=�6�   �� 5        BasicTextBox#��[ 61   $
��Left=�6W   $��
120,83,0,0q�6r   $	��Top=�6�   $��0.77cm��6�   $��6.16cm��6�   ��6   �� *	InputID+6�   �� 5      #��[ 63   $
��Left=�6Y   $��120,123,0,0q�6t   $	��Top=�6�   $��30��6�   $	��233��6�   ��6   �� *	InputPW+6�   �� 5        
BaseButton#��[ 6:   $��CREATE��6^   $
��Left=�6o   $��239,205,0,0q�6�   $	��Top=�6�   $��29��6�   $	��114��6�   ��6   �� *CreateCommand+6�   �� 5        NotifyTextStyle#��[ 6=   $
��Left=�6f   $��
44,218,0,0q�6�   $
��Wrap=�6�   $	��Top=�6�   ��6   �� *NotificationBySign+6�   �� 5      	 �� X1$ 0��6   	 �� Y1$ 65��6   	 �� X2$	 410��6"   	 �� Y2$ 65��6+     MainGridDivideLine#��[ 63   �� 5       #��[ 6   $
��Left=�6E   $��
28,171,0,0q�6`   $
��Wrap=�6t   $	��Top=�6�   �� 6�   $ ko-kr�6�   $�PASS.CONFIRM��6�   6�   �� 5!      #��[ 68   $
��Left=�6^   $��120,164,0,0q�6y   $	��Top=�6�   $��30��6�   $	��233��6�   ��6   �� *InputPWConfirm+6�   5#      5%      5&      