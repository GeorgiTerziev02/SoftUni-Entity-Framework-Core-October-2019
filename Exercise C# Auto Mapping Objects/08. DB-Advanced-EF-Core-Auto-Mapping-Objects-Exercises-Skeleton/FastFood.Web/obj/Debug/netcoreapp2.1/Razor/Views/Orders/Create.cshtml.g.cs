#pragma checksum "D:\softuni\Entity Framework Core\Exercise C# Auto Mapping Objects\08. DB-Advanced-EF-Core-Auto-Mapping-Objects-Exercises-Skeleton\FastFood.Web\Views\Orders\Create.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a06624aa552308cc383b2fafb6283a0711ace92f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Orders_Create), @"mvc.1.0.view", @"/Views/Orders/Create.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Orders/Create.cshtml", typeof(AspNetCore.Views_Orders_Create))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\softuni\Entity Framework Core\Exercise C# Auto Mapping Objects\08. DB-Advanced-EF-Core-Auto-Mapping-Objects-Exercises-Skeleton\FastFood.Web\Views\_ViewImports.cshtml"
using FastFood.Web;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a06624aa552308cc383b2fafb6283a0711ace92f", @"/Views/Orders/Create.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6e2355b4d2dd102d586b09f0f668ac669855f614", @"/Views/_ViewImports.cshtml")]
    public class Views_Orders_Create : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<FastFood.Web.ViewModels.Orders.CreateOrderViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("mx-auto half-width"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Orders", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(60, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "D:\softuni\Entity Framework Core\Exercise C# Auto Mapping Objects\08. DB-Advanced-EF-Core-Auto-Mapping-Objects-Exercises-Skeleton\FastFood.Web\Views\Orders\Create.cshtml"
  
    ViewData["Title"] = "Create Order";

#line default
#line hidden
            BeginContext(110, 83, true);
            WriteLiteral("<h1 class=\"text-center\">Create Order</h1>\r\n<hr class=\"bg-secondary half-width\" />\r\n");
            EndContext();
            BeginContext(193, 1316, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ea51f38c72d14932b77638e19a09f3ec", async() => {
                BeginContext(284, 346, true);
                WriteLiteral(@"
    <div class=""form-group"">
        <label for=""name"">Customer</label>
        <input type=""text"" class=""form-control"" id=""customer"" placeholder=""Customer..."" name=""customer"">
    </div>
    <div class=""form-group"">
        <label for=""employee"">Employee Id</label>
        <select id=""employee"" class=""form-control"" name=""EmployeeId"">
");
                EndContext();
#line 16 "D:\softuni\Entity Framework Core\Exercise C# Auto Mapping Objects\08. DB-Advanced-EF-Core-Auto-Mapping-Objects-Exercises-Skeleton\FastFood.Web\Views\Orders\Create.cshtml"
             foreach (var employee in Model.Employees)
            {

#line default
#line hidden
                BeginContext(701, 16, true);
                WriteLiteral("                ");
                EndContext();
                BeginContext(717, 44, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a5db3ebf5f944f75aafd633ae0df217d", async() => {
                    BeginContext(744, 8, false);
#line 18 "D:\softuni\Entity Framework Core\Exercise C# Auto Mapping Objects\08. DB-Advanced-EF-Core-Auto-Mapping-Objects-Exercises-Skeleton\FastFood.Web\Views\Orders\Create.cshtml"
                                     Write(employee);

#line default
#line hidden
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
#line 18 "D:\softuni\Entity Framework Core\Exercise C# Auto Mapping Objects\08. DB-Advanced-EF-Core-Auto-Mapping-Objects-Exercises-Skeleton\FastFood.Web\Views\Orders\Create.cshtml"
                   WriteLiteral(employee);

#line default
#line hidden
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(761, 2, true);
                WriteLiteral("\r\n");
                EndContext();
#line 19 "D:\softuni\Entity Framework Core\Exercise C# Auto Mapping Objects\08. DB-Advanced-EF-Core-Auto-Mapping-Objects-Exercises-Skeleton\FastFood.Web\Views\Orders\Create.cshtml"
            }

#line default
#line hidden
                BeginContext(778, 167, true);
                WriteLiteral("        </select>\r\n    </div>\r\n    <div class=\"form-group\">\r\n        <label for=\"item\">Item Id</label>\r\n        <select id=\"item\" class=\"form-control\" name=\"ItemId\">\r\n");
                EndContext();
#line 25 "D:\softuni\Entity Framework Core\Exercise C# Auto Mapping Objects\08. DB-Advanced-EF-Core-Auto-Mapping-Objects-Exercises-Skeleton\FastFood.Web\Views\Orders\Create.cshtml"
             foreach (var item in Model.Items)
            {

#line default
#line hidden
                BeginContext(1008, 16, true);
                WriteLiteral("                ");
                EndContext();
                BeginContext(1024, 36, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b3bce130f4d34c15972347d0989baff6", async() => {
                    BeginContext(1047, 4, false);
#line 27 "D:\softuni\Entity Framework Core\Exercise C# Auto Mapping Objects\08. DB-Advanced-EF-Core-Auto-Mapping-Objects-Exercises-Skeleton\FastFood.Web\Views\Orders\Create.cshtml"
                                 Write(item);

#line default
#line hidden
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
#line 27 "D:\softuni\Entity Framework Core\Exercise C# Auto Mapping Objects\08. DB-Advanced-EF-Core-Auto-Mapping-Objects-Exercises-Skeleton\FastFood.Web\Views\Orders\Create.cshtml"
                   WriteLiteral(item);

#line default
#line hidden
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1060, 2, true);
                WriteLiteral("\r\n");
                EndContext();
#line 28 "D:\softuni\Entity Framework Core\Exercise C# Auto Mapping Objects\08. DB-Advanced-EF-Core-Auto-Mapping-Objects-Exercises-Skeleton\FastFood.Web\Views\Orders\Create.cshtml"
            }

#line default
#line hidden
                BeginContext(1077, 425, true);
                WriteLiteral(@"        </select>
    </div>
    <div class=""form-group"">
        <label for=""quantity"">Quantity</label>
        <input type=""number"" step=""1"" min=""1"" class=""form-control"" id=""quantity"" placeholder=""Quantity..."" name=""quantity"">
    </div>
    <hr class=""bg-secondary half-width"" />
    <div class=""button-holder d-flex justify-content-center"">
        <button type=""submit"" class=""btn "">Create</button>
    </div>
");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<FastFood.Web.ViewModels.Orders.CreateOrderViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
