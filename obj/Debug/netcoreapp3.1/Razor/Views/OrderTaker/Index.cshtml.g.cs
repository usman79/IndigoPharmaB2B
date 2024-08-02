#pragma checksum "C:\Users\usman\Desktop\Indigo\IndigoAdmin\IndigoAdmin\Views\OrderTaker\Index.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "99f86db6a82be323af1eabaec2b97e45886cf8c39736573ba58a3aeae6bc6651"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_OrderTaker_Index), @"mvc.1.0.view", @"/Views/OrderTaker/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\usman\Desktop\Indigo\IndigoAdmin\IndigoAdmin\Views\_ViewImports.cshtml"
using IndigoAdmin;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\usman\Desktop\Indigo\IndigoAdmin\IndigoAdmin\Views\_ViewImports.cshtml"
using IndigoAdmin.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA256", @"99f86db6a82be323af1eabaec2b97e45886cf8c39736573ba58a3aeae6bc6651", @"/Views/OrderTaker/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA256", @"9864fdf30c5bd1637cbce9c77a5d6704fec2b497f3e5e9bcfcca243cef647f54", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_OrderTaker_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("userForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\usman\Desktop\Indigo\IndigoAdmin\IndigoAdmin\Views\OrderTaker\Index.cshtml"
  
    ViewData["Title"] = "Orders";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""main-content"">
    <!-- Page Title Start -->
    <div class=""row"">
        <div class=""colxl-12 col-lg-12 col-md-12 col-sm-12 col-12"">
            <div class=""page-title-wrapper"">
                <div class=""page-title-box ad-title-box-use"">
                    <h4 class=""page-title"">Order Takers</h4>
                </div>
                <div class=""ad-breadcrumb"">
                    <ul>
                        <li>
                            <div class=""ad-user-btn"">
                                <input class=""form-control"" type=""text"" placeholder=""Search Here..."" id=""text-input"">
                                <svg version=""1.1"" xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 56.966 56.966"">
                                    <path d=""M55.146,51.887L41.588,37.786c3.486-4.144,5.396-9.358,5.396-14.786c0-12.682-10.318-23-23-23s-23,10.318-23,23
												s10.318,23,23,23c4.761,0,9.298-1.436,13.177-4.162l13.661,14.208c0.571,0.593,1.339,0.92,2.162,0.92
												c0.779,0,1");
            WriteLiteral(@".518-0.297,2.079-0.837C56.255,54.982,56.293,53.08,55.146,51.887z M23.984,6c9.374,0,17,7.626,17,17s-7.626,17-17,17
												s-17-7.626-17-17S14.61,6,23.984,6z""></path>
                                </svg>
                            </div>
                        </li>
                        <li>
                            <div class=""add-group"">
                                <a class=""ad-btn"" onclick=""OpenNewModel()"">New Account</a>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <!-- Table Start -->
    <div class=""row"">
        <!-- Styled Table Card-->
        <div class=""col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12"">
            <div class=""card table-card"">
                <div class=""card-header pb-0"">
                    <h4>Order Takers</h4>
                </div>
                <div class=""card-body"">
                    <table id=""dtOrderTakers"" class");
            WriteLiteral(@"=""table table-striped table-bordered dt-responsive nowrap"" style=""width:100%"">
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Name</th>
                                <th>Email</th>
                                <th>Status</th>
                                <th>Orders</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                    </table>
                </div>
            </div>
        </div>
    </div>
</div>

");
            DefineSection("AppModals", async() => {
                WriteLiteral(@"
    <div class=""modal fade"" id=""exampleModalLong"" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalLongTitle"" aria-hidden=""true"">
        <div class=""modal-dialog modal-lg"" role=""document"">
            <div class=""modal-content"">
                <div class=""modal-header"">
                    <h5 class=""modal-title"" id=""exampleModalLongTitle"">New Account</h5>
                    <button type=""button"" class=""close"" data-bs-dismiss=""modal"" aria-label=""Close"">
                        <span aria-hidden=""true"">&times;</span>
                    </button>
                </div>
                <div class=""modal-body"">
                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "99f86db6a82be323af1eabaec2b97e45886cf8c39736573ba58a3aeae6bc66517535", async() => {
                    WriteLiteral(@"
                        <div class=""row"">
                            <div class=""col-xl-6 col-lg-6 col-md-6 col-sm-6 col-6"">
                                <div class=""form-group"">
                                    <label class=""col-form-label"">Firat Name</label>
                                    <input required=""required"" id=""UserFirstName"" class=""form-control required"" type=""text"" placeholder=""First Name"">
                                </div>
                            </div>
                            <div class=""col-xl-6 col-lg-6 col-md-6 col-sm-6 col-6"">
                                <div class=""form-group"">
                                    <label class=""col-form-label"">Last Name</label>
                                    <input required=""required"" id=""UserLastName"" class=""form-control required"" type=""text"" placeholder=""Last Name"">
                                </div>
                            </div>
                            <div class=""col-xl-6 col-lg-6 col-md-6 col");
                    WriteLiteral(@"-sm-6 col-6"">
                                <div class=""form-group"">
                                    <label class=""col-form-label"">Email</label>
                                    <input required=""required"" id=""UserEmailAddress"" class=""form-control required"" type=""text"" placeholder=""Email Address"">
                                </div>
                            </div>
                            <div class=""col-xl-6 col-lg-6 col-md-6 col-sm-6 col-6"">
                                <div class=""form-group"">
                                    <label class=""col-form-label"">Password</label>
                                    <input required=""required"" id=""UserPassword"" class=""form-control required"" type=""password"" placeholder=""Password"">
                                </div>
                            </div>
                            <div class=""col-xl-6 col-lg-6 col-md-6 col-sm-6 col-6"">
                                <div class=""form-group"">
                                    <la");
                    WriteLiteral(@"bel class=""col-form-label"">Phone Number</label>
                                    <input required=""required"" id=""UserPhone"" class=""form-control required"" type=""text"" placeholder=""Phone Number"">
                                </div>
                            </div>
                            <div class=""col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12"">
                                <div class=""form-group"">
                                    <label class=""col-form-label"">Address</label>
                                    <textarea required=""required"" id=""Address"" class=""form-control required"" type=""text"" placeholder=""Address""></textarea>
                                </div>
                            </div>
                            <input type=""hidden"" id=""UserId"" />
                        </div>
                    ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                </div>
                <div class=""modal-footer"">
                    <button type=""button"" class=""btn btn-secondary"" data-bs-dismiss=""modal"">Close</button>
                    <button type=""button"" class=""btn btn-primary"" onclick=""SaveItem()"">Save changes</button>
                </div>
            </div>
        </div>
    </div>
");
            }
            );
            WriteLiteral("\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script>
        var dtOrderTakers = null;
        var EditObject = null;

        $(document).ready(function () {
            LoadAll();
        });

        function LoadAll() {
            var host = window.location;
            $.ajax({
                type: ""GET"",
                contentType: ""application/json; charset=utf-8"",
                url: '/OrderTaker/GetAllOrderTakers',
                success: function (data) {
                    BindDataTable(data);
                },
                error: function (result) {
                }
            });
        }

        function BindDataTable(data) {
            if (dtOrderTakers != null)
                dtOrderTakers.destroy();
            dtOrderTakers = $('#dtOrderTakers').DataTable({
                dom: ""<'row'<'col-sm-12'tr>>"" + ""<'row'<'col-sm-4'i><'col-sm-8'p>>"",
                data: data,
                order: false,
                autoWidth: false,
                pageLength: 9,
                rowId");
                WriteLiteral(@": ""UserId"",
                columns: [ {
                        ""searchable"": false,
                        data: ""UserId"",
                        class: ""no-sort"",
                        ""orderable"": false,
                          
                    },

                    {
                        ""searchable"": false,
                        data: null,
                        class: ""no-sort"",
                        ""orderable"": false,
                        render: function (data, type, row, meta) {
                            return row.UserFirstName + "" "" + row.UserLastName;
                        }
                    },
                    {
                        data: ""UserEmailAddress"",
                        ""orderable"": false,
                        class: ""no-sort"",
                        ""searchable"": false
                    },
                    {
                        data: ""ActiveStatus"",
                        ""orderable"": false,
             ");
                WriteLiteral(@"           class: ""no-sort"",
                        ""searchable"": false,
                        render: function (data, type, row, meta) {
                            var label = '';
                            if (row.ActiveStatus == 1) {
                                label = '<label class=""mb-0 badge badge-success toltiped"" data-bs-toggle=""dropdown"">Active</label>';
                            }
                            else {
                                label = '<label class=""mb-0 badge badge-danger toltiped"" data-bs-toggle=""dropdown"">InActive</label>';
                            }
                            return '<div class=""dropdown""> ' + label + ' <ul class=""dropdown-menu""> <li><a class=""dropdown-item"" href=""#"" onclick=""UpdateUserStatus(this,true)""><label class=""mb-0 badge badge-success w-100"">Active</label></a></li> <li><a class=""dropdown-item"" href=""#"" onclick=""UpdateUserStatus(this,false)""><label class=""mb-0 badge badge-danger w-100"">InActive</label></a></li>  </ul> </div>';");
                WriteLiteral(@"
                        }
                    },

                    {
                        ""searchable"": false,
                        data: ""Orders"",
                        class: ""no-sort"",
                        ""orderable"": false,
                        render: function (data, type, row, meta) {
                            var classBadge = 'badge-danger';
                            if (row.Orders > 0) {
                                classBadge = 'badge-success';
                            }
                            return '<label class=""mb-0 badge icon-badge ' + classBadge + ' toltiped ml-2"" data-original-title=""Insufficient Stock "">' + row.Orders + '</label>';
                        }
                    },
                    {
                        data: null,
                        ""searchable"": false,
                        class: ""no-sort text-center"",
                        ""orderable"": false,
                        render: function (data, type, row, met");
                WriteLiteral(@"a) {
                            var html = '<div class=""relative""><a class=""action-btn"" href=""javascript:void(0);""> <svg class=""default-size "" viewBox=""0 0 341.333 341.333 ""> <g> <g> <g> <path d=""M170.667,85.333c23.573,0,42.667-19.093,42.667-42.667C213.333,19.093,194.24,0,170.667,0S128,19.093,128,42.667 C128,66.24,147.093,85.333,170.667,85.333z ""></path> <path d=""M170.667,128C147.093,128,128,147.093,128,170.667s19.093,42.667,42.667,42.667s42.667-19.093,42.667-42.667 S194.24,128,170.667,128z ""></path> <path d=""M170.667,256C147.093,256,128,275.093,128,298.667c0,23.573,19.093,42.667,42.667,42.667s42.667-19.093,42.667-42.667 C213.333,275.093,194.24,256,170.667,256z ""></path> </g> </g> </g> </svg> </a> <div class=""action-option""> <ul> <li> <a onclick=""EditItem(this)"" href=""javascript:void(0);""><i class=""far fa-edit mr-2""></i>Edit</a> </li> </ul> </div></div>';
                            return html;
                        }
                    }
                ]
            });
        }

        func");
                WriteLiteral(@"tion OpenNewModel() {
            $('#UserId').val('');
            $('#UserFirstName').val('');
            $('#UserLastName').val('');
            $('#UserEmailAddress').val('');
            $('#UserPassword').val('');
            $('#Address').val('');
            $('#UserPhone').val('');

            $(""#exampleModalLong"").modal(""show"");
        }

        function EditItem(element) {
            var data = dtOrderTakers.row($(element).parents('tr')).data();
            $('#UserId').val(data.UserId);
            $('#UserFirstName').val(data.UserFirstName);
            $('#UserLastName').val(data.UserLastName);
            $('#UserEmailAddress').val(data.UserEmailAddress);
            $('#UserPassword').val(data.UserPassword);
            $('#Address').val(data.Address);
            $('#UserPhone').val(data.UserPhone);

            $(""#exampleModalLong"").modal(""show"");
        }

        function DeleteItem(id) {
            var host = window.location;
            $.ajax({
     ");
                WriteLiteral(@"           url: host.protocol + ""//"" + host.host + '/Brand/Delete?id=' + id,
                type: ""DELETE"",
                contentType: ""application/json; charset=utf-8"",
                success: function (json) {
                    LoadAll();
                },
                error: function (result) {

                }
            });
        }

        function SaveItem() {
            if (CheckRequired($('#userForm'))) {
                var UserInfo = {
                    UserId: $('#UserId').val(),
                    UserFirstName: $('#UserFirstName').val(),
                    UserLastName: $('#UserLastName').val(),
                    UserEmailAddress: $('#UserEmailAddress').val(),
                    UserPassword: $('#UserPassword').val(),
                    UserPhone: $('#UserPhone').val(),
                    Address: $('#Address').val()
                };
                var formData = new FormData();
                formData.append('UserId', UserInfo.UserId);
     ");
                WriteLiteral(@"           formData.append('UserFirstName', UserInfo.UserFirstName);
                formData.append('UserLastName', UserInfo.UserLastName);
                formData.append('UserEmailAddress', UserInfo.UserEmailAddress);
                formData.append('UserPassword', UserInfo.UserPassword);
                formData.append('UserPhone', UserInfo.UserPhone);
                formData.append('Address', UserInfo.Address);


                $.ajax({
                    url: '/OrderTaker/Save',
                    type: ""POST"",
                    contentType: false,
                    processData: false,
                    data: formData,
                    success: function (result) {
                        $(""#exampleModalLong"").modal(""hide"");
                        LoadAll();
                    },
                    error: function (err) {
                        alert(err.statusText);
                    }
                });
            } else {
                alert('Please fill ");
                WriteLiteral(@"all required fields!');
            }
        }

        function CheckRequired($element) {
            var $form = $element;

            if ($form.find('.required').filter(function () { return this.value === '' }).length > 0) {
                return false;
            }
            return true;
        }

        function UpdateUserStatus(element, status) {
            var dataUser = dtOrderTakers.row($(element).parents('tr')).data();
            dataUser.ActiveStatus = status;

            $.ajax({
                type: ""GET"",
                contentType: ""application/json; charset=utf-8"",
                url: '/OrderTaker/UpdateStatus?UserId=' + dataUser.UserId + '&&Status=' + dataUser.ActiveStatus,
                success: function (data) {
                    if (data == true) {

                        dtOrderTakers.row('#' + dataUser.UserId)
                            .data(dataUser)
                            .draw();
                    }
                },
          ");
                WriteLiteral("      error: function (result) {\r\n                }\r\n            });\r\n        }\r\n\r\n    </script>\r\n");
            }
            );
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
