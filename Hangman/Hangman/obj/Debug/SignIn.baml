   M S B A M L   `   `   `  ����B  >Hangman, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:HangmanHangman  5   	   7 clr-namespace:Hangman.ViewModelsHangman.ViewModels  5   	       Hangman.SignIn   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Hangman     5   	   7
viewmodels clr-namespace:Hangman.ViewModels     5   	     � Title$  SignIn��5	   	   $	��450��6   $	��800��6%    � DataContext 5
      &    Hangman.ViewModels.UserViewModel  5   
   ��5      �� 5   
    �� ImageSource$# /Resources/backgroundGame.jpg��6    j� Stretch$
 Fill=�6A   .��5      � .+�5   
   �� $��New User��6   $
��Left=�6D   $	��Top=�6_   $��75��6w   $��
10,223,0,0q�6�   ��6$   �� *
AddCommand+6�   �� 5   
   -   5        $��
DeleteUser��5   '   $��Delete User��6   $
��Left=�6[   $	��Top=�6v   $��75��6�   $��128,223,0,0q�6�   ��69   �� *DeleteCommand+6�   �� 5   
   -   5        $��PlayGame��5       $
��Play��6   $
��Left=�6P   $	��Top=�6k   $��75��6�    J� RenderTransformOrigin$ 2.415,7.457&�6�   $��512,348,0,0q�6�   ��60   �� *PlayCommand+6�   �� 5   
   -   5        $��Avatar��5   3   $��Choose avatar��6A   $
��Left=�6Y   $	��Top=�6t   $	��106��6�   $ 6.872,7.907&�6�   $��550,152,0,0q�6�   ��6   �� *AvatarCommand+6�   �� 5   
   $��Cancel��6   $
��Left=�6C   $	��Top=�6^   $��75��6v   $��651,348,0,0q�6�   ��6"   �� *CloseCommand+6�   �� 5   
   $
��Left=�6;   $	��100��6V   $	��Top=�6c   $	��165��6{   $��
550,34,0,0q�6�   ��6   �� *CurrentUser.ImagePath+6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��dataGrid��5      $
��Left=�6%   $	��138��6@   $��	10,34,0,0q�6M   $	��Top=�6`   $	��407��6x      AutoGenerateColumns .  ��6�   �� *Users+ �� SelectedItem 6�   �� *CurrentUser+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly . 	   Header$
	 Nume��67   
   Width5  /System.Windows.Controls.DataGridLengthConverter$	
 100 6b   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6E   �� *
UserName+6b     5       . $	 	ImagePath��67   $	
 300 6h    6J   �� *	ImagePath+6h      RowStyle 5      �� 5        �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�5!      -   5        5#      5%      5(      5*      