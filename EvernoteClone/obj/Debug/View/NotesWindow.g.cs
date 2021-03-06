#pragma checksum "..\..\..\View\NotesWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "927E443F707E80B0B696C32E4F3A6E424397F43C9105B35BEF32CAA784DF94B8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using EvernoteClone.View.UserControls;
using EvernoteClone.ViewModel;
using Microsoft.Xaml.Behaviors;
using Microsoft.Xaml.Behaviors.Core;
using Microsoft.Xaml.Behaviors.Input;
using Microsoft.Xaml.Behaviors.Layout;
using Microsoft.Xaml.Behaviors.Media;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace EvernoteClone.View {
    
    
    /// <summary>
    /// NotesWindow
    /// </summary>
    public partial class NotesWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 34 "..\..\..\View\NotesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock statusTextBlock;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\View\NotesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton boldButton;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\View\NotesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton italicButton;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\View\NotesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton underlineButton;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\..\View\NotesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox fontFamilyComboBox;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\..\View\NotesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox fontSizeComboBox;
        
        #line default
        #line hidden
        
        
        #line 124 "..\..\..\View\NotesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RichTextBox contentRichTextBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/EvernoteClone;component/view/noteswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\NotesWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 29 "..\..\..\View\NotesWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.statusTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            
            #line 93 "..\..\..\View\NotesWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SpeechButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.boldButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 95 "..\..\..\View\NotesWindow.xaml"
            this.boldButton.Click += new System.Windows.RoutedEventHandler(this.boldButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.italicButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 100 "..\..\..\View\NotesWindow.xaml"
            this.italicButton.Click += new System.Windows.RoutedEventHandler(this.italicButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.underlineButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 105 "..\..\..\View\NotesWindow.xaml"
            this.underlineButton.Click += new System.Windows.RoutedEventHandler(this.underlineButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.fontFamilyComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 111 "..\..\..\View\NotesWindow.xaml"
            this.fontFamilyComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.fontFamilyComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.fontSizeComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 115 "..\..\..\View\NotesWindow.xaml"
            this.fontSizeComboBox.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent, new System.Windows.Controls.TextChangedEventHandler(this.fontSizeComboBox_TextChanged));
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 117 "..\..\..\View\NotesWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.contentRichTextBox = ((System.Windows.Controls.RichTextBox)(target));
            
            #line 125 "..\..\..\View\NotesWindow.xaml"
            this.contentRichTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.contentRichTextBox_TextChanged);
            
            #line default
            #line hidden
            
            #line 126 "..\..\..\View\NotesWindow.xaml"
            this.contentRichTextBox.SelectionChanged += new System.Windows.RoutedEventHandler(this.contentRichTextBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

