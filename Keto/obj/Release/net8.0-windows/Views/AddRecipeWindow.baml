   M S B A M L   `   `   `  ����   Keto     Keto.Views.AddRecipeWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Dodaj Przepis��$	��700��$	��500��.��� $��20q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $	��150 ��� $��* �.+��� $��Nazwa przepisu:��$��0��$��0��$��Center=�$��10q��� -    ��Name$ RecipeNameTextBox��$��0��$��1��$��10q�$	��250���� $��Białko (g):��$��1��$��0��$��Center=�$��10q��� -   $ ProteinTextBox��$��1��$��1��$��10q�$	��250���� $��Tłuszcz (g):��$��2��$��0��$��Center=�$��10q��� -   $ 
FatTextBox��$��2��$��1��$��10q�$	��250���� $��Węglowodany (g):��$��3��$��0��$��Center=�$��10q��� -   $ CarbsTextBox��$��3��$��1��$��10q�$	��250���� $��Węglowodany netto (g):��$��4��$��0��$��Center=�$��10q��� -   $ NetCarbsTextBox��$��4��$��1��$��10q�$	��250���� $��Kalorie (kcal):��$��5��$��0��$��Center=�$��10q��� -   $ CaloriesTextBox��$��5��$��1��$��10q�$	��250���� $��Składniki:��$��6��$��0��$��Center=�$��10q��� -   $ IngredientsTextBox��$��6��$��1��$��10q�$��60��$	��250�� �� TextWrapping$
 Wrap=� �� AcceptsReturn$
 True���� $��Ilość porcji:��$��7��$��0��$��Center=�$��10q��� -   $ ServingsTextBox��$��7��$��1��$��10q�$	��250���� $��Instrukcja przygotowania:��$��8��$��0��$��Center=�$��10q��� -	   $ InstructionsTextBox��$��8��$��1��$��10q�$	��100��$	��250��$
 Wrap=�$
 True���� $��Kategoria przepisu:��$��9��$��0��$��Center=�$��10q��� -
    ��Name$ CategoryComboBox��$��9��$��1��$��10q�$	��250��.@��� $��Śniadanie i Kolacja���� $��Obiad���� $��Desery����  �� Orientation$ 
Horizontal=�$��10��$��2��$��Center=�$��20q�.+��� -   $��Zapisz��$	��100��$��30��$��10q��� -   $��Anuluj��$	��100��$��30��$��10q�