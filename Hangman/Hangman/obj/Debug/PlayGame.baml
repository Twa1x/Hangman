   M S B A M L   `   `   `  ����B  >Hangman, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:HangmanHangman  5   	   7 clr-namespace:Hangman.ViewModelsHangman.ViewModels  5      7 clr-namespace:Hangman.ViewModelsHangman.ViewModels  5          Hangman.PlayGame   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Hangman     5   	   7
viewmodels clr-namespace:Hangman.ViewModels     5      8viewmodels2 clr-namespace:Hangman.ViewModels     5        � Title$  PlayGame��5   	   $	��450��6   $	��800��6'    � DataContext 5      &    Hangman.ViewModels.UserViewModel  5   
   ��5      �� 5   
    �� ImageSource$# /Resources/backgroundGame.jpg��6    j� Stretch$
 Fill=�6A   .��5      � -   5         �Name$ GameGrid��5      $	��495��6   $	��320��6)   +�5   
   �� 5      $ ��       CHOOSE THE CATEGORY��6   $
��Left=�69   $��28��6T   $��0,64,0,0q�6`   $	��Top=�6r   $	��185��6�   
���������5      �� 5       �� EndPoint$ 0.5,1&�6*    �� 
StartPoint$ 0.5,0&�6;   .��5      �  � Color$ Black��6'   � 5      $ 	#FFE01D1D��6'    � Offset$ 1Q�69   5      5      �� 5      $ ��       WELCOME TO HANGMAN!��6   $
��Left=�69   $��49��6T   $��
-12,15,0,0q�6`   $	��Top=�6t   $	��364��6�   
�������$��24(�6�   $��Italic$�6�   $��Viner Hand ITC)�6�   ��5       �� 5!      $ 0.5,1&�6*   $ 0.5,0&�6;   .��5"      � $ Black��6'   � 5#      $ 	#FFE00C0C��6'   $ 1Q�69   5$      5&      �� 5'      -   5        $��AllCategories��5'   -   $��AllCategories��6   $
��Left=�6B   $	��Top=�6]   $	��175��6u   $��	10,92,0,0q�6�   6�   �� 5(      -   5        $��Movies��5(   &   $��Movies��6   $
��Left=�64   $	��Top=�6O   $	��175��6g   $��
10,117,0,0q�6s   6�   �� 5)      -   5        $
��Cars��5)   %   $
��Cars��6   $
��Left=�62   $	��Top=�6M   $	��175��6e   $��
10,142,0,0q�6q   6�   �� 5*      -   5        $��States��5*   &   $��States��6   $
��Left=�64   $	��Top=�6O   $	��175��6g   $��
10,167,0,0q�6s   6�   �� 5+      -   5        $��	Mountains��5+   )   $��	Mountains��6   $
��Left=�6:   $	��Top=�6U   $	��175��6m   $��
10,192,0,0q�6y   6�   �� 5,      -   5        $��Rivers��5,   '   $��Rivers��6   $
��Left=�66   $	��Top=�6Q   $	��175��6i   $��
10,217,0,0q�6u   6�   �� 5-      -   5        $��Refresh��5-      $
��Left=�6'   $	��Top=�6B   $��75��6Z   	 J� RenderTransformOrigin$	 0.716,2.579&�6e   $��
61,242,0,0q�6�   6�   �� 5.      $��About��6   $
��Left=�6G   $	��Top=�6b   $��75��6z   $��132,290,0,0q�6�   ��6&   �� *HelpCommand+6�   �� 5/      $��
Statistics��6   $
��Left=�6P   $	��Top=�6k   $��75��6�   $��
10,290,0,0q�6�   ��6*   �� *StatisticsCommand+6�   �� 50      -	   5        $
��Exit��50      $
��Left=�6$   $	��Top=�6?   $��75��6W   $��360,290,0,0q�6b   6w   �� 51      -
   5        $��OpenGameBtn��51      $��	Open Game��6*   $��346,196,0,89q�6>   $	 -0.287,1.107&�6T   6z   �� 52      -   5        $��Imagine��52      $	��100��6%   $	��100��62   �� 53      -   5        $��	labelNume��53      $�� ��6%   $
��Left=�60   $��28��6K   $��212,217,0,0q�6W   $	��Top=�6l   $	��186��6�   
�������$��14(�6�   $��Italic$�6�   ��54      �� 55      $ 0.5,1&�6*   $ 0.5,0&�6;   .��56      � $ Black��6'   � 57      $ 	#FFE01D1D��6'   $ 1Q�69   58      5:      5A      5B      