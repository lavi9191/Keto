// Updated by XamlIntelliSenseFileGenerator 02.11.2024 17:01:46
#pragma checksum "..\..\..\..\..\Views\MealPlan\WeeklyPlanView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "DC2F4A31D4844366DEA4D0BD8E43D35BD9295645"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Keto.Views.MealPlan
{


    /// <summary>
    /// WeeklyPlanView
    /// </summary>
    public partial class WeeklyPlanView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector
    {

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.10.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Keto;component/views/mealplan/weeklyplanview.xaml", System.UriKind.Relative);

#line 1 "..\..\..\..\..\Views\MealPlan\WeeklyPlanView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.WeeklyPlanGrid = ((System.Windows.Controls.DataGrid)(target));
                    return;
                case 2:

#line 27 "..\..\..\..\..\Views\MealPlan\WeeklyPlanView.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddPlan_Click);

#line default
#line hidden
                    return;
                case 3:

#line 28 "..\..\..\..\..\Views\MealPlan\WeeklyPlanView.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EditPlan_Click);

#line default
#line hidden
                    return;
                case 4:

#line 29 "..\..\..\..\..\Views\MealPlan\WeeklyPlanView.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RemovePlan_Click);

#line default
#line hidden
                    return;
                case 5:

#line 30 "..\..\..\..\..\Views\MealPlan\WeeklyPlanView.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SavePlan_Click);

#line default
#line hidden
                    return;
                case 6:

#line 31 "..\..\..\..\..\Views\MealPlan\WeeklyPlanView.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ExportToPDF_Click);

#line default
#line hidden
                    return;
                case 7:

#line 32 "..\..\..\..\..\Views\MealPlan\WeeklyPlanView.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ExportToWord_Click);

#line default
#line hidden
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.DataGrid WeeklyMealsGrid;
    }
}

