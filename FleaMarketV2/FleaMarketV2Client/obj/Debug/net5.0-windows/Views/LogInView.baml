   M S B A M L   `   `   `  ����M  IFleaMarketV2Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:FleaMarketV2Client.ViewsFleaMarketV2Client.Views  5   	   G(clr-namespace:FleaMarketV2Client.ServiceFleaMarketV2Client.Service  5   	   K*clr-namespace:FleaMarketV2Client.ViewModelFleaMarketV2Client.ViewModel  6A   (    "FleaMarketV2Client.Views.LogInView   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:FleaMarketV2Client.Views     5   	   ;locals(clr-namespace:FleaMarketV2Client.Service     5   	   @	viewmodel*clr-namespace:FleaMarketV2Client.ViewModel     6A     � Title$  
MainWindow��5	   	   $��6.82cm��6   $��10.84cm��6,    � WindowStyle$
 None=�6<    � 
ResizeMode$ CanMinimize=�6O   .��5
      �    BackGroundBrush1#��[  6   $��10.84cm��6:   $��6.82cm��6J   .+�5   
   ��   MainLogoTextStyle#��[ 6   $
��Left=�6@   $��17,0,0,0q�6[   $
��Wrap=�6m   $	��Top=�6�   $	��240��6�   �� 6�   $�FLEA MARKET��6�   �� 6�   �� 6�   $�
E-COMMERCE��6�   6�   �� 5   
     BaseTextBlockStyle#��[ 6   $
��Left=�6@   $��	28,90,0,0q�6[   $��USERNAME��6n   $	��Top=�6~   �� 5   
   #��[ 6   $
��Left=�6A   $��
28,130,0,0q�6\   $��PASSWORD��6p   $
��Wrap=�6�   $	��Top=�6�   �� 5   
     BasicTextBox#��[ 6   $
��Left=�68   $��
120,83,0,0q�6S   $	��Top=�6   $��0.77cm��6�   $��6.16cm��6�   ��6g   �� *UserID+6�   G� 5   
   -   5         G�Name$ pwbox��5        
BasicPWBox#��[ 6e   $
��Left=�6�   $��120,123,0,0q�6�   $	��Top=�6�   $��30��6�   $	��233��6�   $��LeftToRight=�6�   &    FleaMarketV2Client.Service.PWBox   BindablePassword 6'   �� *UserPW+ �� Mode$ TwoWay=�6  �� 5   
     
BaseButton#��[ 6   $��SIGN-IN��68   $
��Left=�6J   $��120,169,0,0q�6e   $	��Top=�6z   $��29��6�   $	��114��6�   ��6�   �� *SignInCommand+�� 5   
   #��[ 6   $��LOG-IN��66   $
��Left=�6G   $��239,169,0,0q�6b   $	��Top=�6w   $��29��6�   $	��114��6�   ��6�   �� *LogInBtnCommand+�� 5   
     NotifyTextStyle#��[ 6   $
��Left=�6>   $��177,226,0,0q�6Y   $
��Wrap=�6�   $	��Top=�6�   ��6n   �� *Notification+6�   �� 5   
   	 �� X1$ 0��6   	 �� Y1$ 65��6   	 �� X2$	 410��6   		 �� Y2$	 65��6'     MainGridDivideLine#��[ 6/   5      5      