using DaisyPets.Core.Application.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Notifications;
using Syncfusion.Blazor.Popups;
using System.Reflection.Metadata;
using static DaisyPets.Core.Application.Enums.Common;

namespace DaisyPets.Web.Blazor.Pages.Blog
{
    public partial class BlogMainPage
    {
        [Inject]
        IConfiguration? config { get; set; }

        [Inject]
        ILogger<App>? logger { get; set; } = null;

        [Inject]
        public IStringLocalizer<App>? L { get; set; }
        protected PostDto? SelectedPost { get; set; }

        protected IEnumerable<PostDto>? Posts;
        protected string? urlBaseAddress;
        protected string? postsEndpoint;
        protected int PostId { get; set; }
        protected SfToast? ToastObj { get; set; }
        protected OpcoesRegisto RecordMode { get; set; }
        protected string? NewCaption { get; set; }
        protected string? EditCaption { get; set; }

        protected string? DeleteCaption;
        protected Modules Module { get; set; }
        protected bool AddEditPostVisibility { get; set; }
        protected bool DeletePostVisibility { get; set; }
        protected bool ErrorVisibility { get; set; } = false;
        protected bool AlertVisibility { get; set; } = false;
        protected string? WarningMessage { get; set; }
        protected string? WarningTitle { get; set; }
        protected bool WarningVisibility { get; set; }

        protected List<string> ValidationsMessages = new();
        protected string? AlertTitle = "";
        protected string? ToastTitle;
        protected string? ToastMessage;
        protected string? ToastCss;
        protected string? ToastIcon;
        protected string ToastContent = "";
        protected string ToastCssClass = "";
        protected DialogEffect Effect = DialogEffect.Zoom;

        protected int BlogCount;
        protected override async Task OnInitializedAsync()
        {
            urlBaseAddress = config?["ApiSettings:UrlBase"];
            postsEndpoint = $"{urlBaseAddress}/Posts";
            PostId = 0;
            Posts = await GetAllPosts();
            BlogCount = 0;
        }

        protected async Task<IEnumerable<PostDto>?> GetAllPosts()
        {
            var result = await GetData<PostDto>($"{postsEndpoint!}/AllPosts");
            return result.ToList();
        }

        protected async Task<IEnumerable<T>> GetData<T>(string url)
            where T : class
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetFromJsonAsync<IEnumerable<T>>(url);
                    if (response == null)
                    {
                        return Enumerable.Empty<T>();
                    }

                    return response;
                }
                catch (HttpRequestException exa)
                {
                    logger?.LogError(exa.Message, L["MSG_ApiError"]);
                    return Enumerable.Empty<T>();
                }
                catch (Exception ex)
                {
                    logger?.LogError(ex.Message, L["MSG_ApiError"]);
                    return Enumerable.Empty<T>();
                }
            }
        }

        protected async Task RowSelectHandler(RowSelectEventArgs<PostDto> args)
        {
            PostId = args.Data.Id;
            SelectedPost = await GetPostById(PostId);
        }

        protected async Task<PostDto> GetPostById(int Id)
        {
            var url = $"{urlBaseAddress}/Posts/{Id}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var post = await httpClient.GetFromJsonAsync<PostDto>(url);
                    if (post?.Id > 0)
                    {
                        return post;
                    }
                    else
                    {
                        return new PostDto();
                    }
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return new PostDto();
            }
        }

        protected async Task OnEditPost(int postId)
        {
            Module = Modules.Posts;

            SelectedPost = await GetPostById(postId);

            AddEditPostVisibility = true;
            EditCaption = $"{L["EditMsg"]} Post";
            RecordMode = OpcoesRegisto.Gravar;

        }
        public async Task OnPostCommandClicked(CommandClickEventArgs<PostDto> args)
        {
            Module = Modules.Posts;

            if (args.CommandColumn.Type == CommandButtonType.Edit)
            {
                PostId = args.RowData.Id;
                SelectedPost = await GetPostById(PostId);

                AddEditPostVisibility = true;
                EditCaption = $"{L["EditMsg"]} Post";
                RecordMode = OpcoesRegisto.Gravar;
            }

            if (args.CommandColumn.Type == CommandButtonType.Delete)
            {
                WarningTitle = $"{L["DeleteMsg"]} Post?";
                DeletePostVisibility = true;
                DeleteCaption = SelectedPost?.Title;
            }
        }


        public void onAddPost(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            RecordMode = OpcoesRegisto.Inserir;
            NewCaption = $"{L["NewMsg"]} Post";
            Module = Modules.Posts;
            SelectedPost = new()
            {
                BodyText = "",
                Image = "",
                Introduction = "",
                Title = ""
            };
            AddEditPostVisibility = true;
        }

        public async Task<bool> SavePost()
        {
            PostId = SelectedPost.Id;
            var validationMessages = await ValidatePost();
            if (validationMessages.Any())
            {
                ValidationsMessages = validationMessages;
                ErrorVisibility = true;
                return false;
            }

            var url = $"{urlBaseAddress}/Posts";
            var result = await Save(SelectedPost!, url);
            return result;
        }

        protected async Task<List<string>> ValidatePost()
        {
            var validatorEndpoint = $"{urlBaseAddress}/Posts/ValidatePost";
            var errors = await Validate(SelectedPost!, validatorEndpoint);
            return errors.Count() > 0 ? errors : new List<string>();
        }

        protected async Task<List<string>> Validate<T>(T entity, string validatorEndPoint)
            where T : class
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsJsonAsync(validatorEndPoint, entity);
                    if (response.IsSuccessStatusCode == false)
                    {
                        var errors = response.Content.ReadFromJsonAsync<List<string>>().Result;
                        if (errors.Count() > 0)
                        {
                            return errors;
                        }
                        else
                            return new List<string>();
                    }

                    return new List<string>();
                }
            }
            catch (Exception ex)
            {
                return new List<string>()
                {
                    ex.Message
                };
            }
        }

        protected async Task<bool> Save<T>(T dto, string url)
            where T : class
        {
            string? entityTitle = string.Empty;
            int? entityId = 0;
            switch (Module)
            {
                case Modules.Posts:
                    entityTitle = "Post";
                    entityId = PostId;
                    break;
            }

            if (RecordMode == OpcoesRegisto.Gravar)
            {
                ToastTitle = $"{L["btnSalvar"]} {entityTitle}";
                try
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        var result = await httpClient.PutAsJsonAsync($"{url}/{entityId}", dto);
                        var success = result.IsSuccessStatusCode;
                        await DisplaySuccessOrFailResults(success, RecordMode);
                        return success;
                    }
                }
                catch (Exception exc)
                {
                    logger?.LogError(exc.Message, $"{L["MSG_ApiError"]}");
                    return false;
                }
            }
            else // Insert
            {
                ToastTitle = $"{L["creationMsg"]} {entityTitle}";
                try
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        var result = await httpClient.PostAsJsonAsync(url, dto);
                        var success = result.IsSuccessStatusCode;
                        await DisplaySuccessOrFailResults(success, RecordMode);
                        return success;
                    }
                }
                catch (Exception exc)
                {
                    logger?.LogError(exc.Message, $"{L["MSG_ApiError"]}");
                    return false;
                }
            }
        }

        protected async Task DisplaySuccessOrFailResults(bool success, OpcoesRegisto addUpdateOperation)
        {
            // TODO adapt to delete operations
            if (addUpdateOperation == OpcoesRegisto.Gravar) // update
            {
                if (!success)
                {
                    AlertVisibility = true;
                    AlertTitle = L["FalhaGravacaoRegisto"];
                    WarningMessage = L["MSG_ApiError"];
                }
                else
                {
                    ToastCss = "e-toast-success";
                    ToastMessage = L["SuccessUpdate"];
                    ToastIcon = "fas fa-check";
                }
            }
            else // insert
            {
                if (!success)
                {
                    AlertVisibility = true;
                    AlertTitle = L["FalhaCriacaoRegisto"];
                    WarningMessage = L["MSG_ApiError"];
                }
                else
                {
                    ToastCss = "e-toast-success";
                    ToastMessage = L["SuccessInsert"];
                    ToastIcon = "fas fa-check";
                }
            }

            Posts = await GetAllPosts();
            await InvokeAsync(StateHasChanged);
            await Task.Delay(100);
            await ToastObj.ShowAsync();
            AddEditPostVisibility = false;
        }

        protected async Task ConfirmDeleteYes()
        {
            ToastTitle = $"{L["DeleteMsg"]} Post";
            await DeletePost();
            ToastCssClass = "e-toast-success";
            ToastContent = L["RegistoAnuladoSucesso"];
            await Task.Delay(200);
            await ToastObj!.ShowAsync();
        }

        protected async Task DeletePost()
        {
            string url = $"{urlBaseAddress}/Posts/{PostId}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var response = await httpClient.DeleteAsync(url);
                    response.EnsureSuccessStatusCode();
                    if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        ValidationsMessages = new List<string>()
                        {
                            L["FalhaAnulacaoRegisto"]
                        };
                        ErrorVisibility = true;
                        return;
                    }

                    Posts = await GetAllPosts();
                }

                DeletePostVisibility = false;
                AlertTitle = "Apagar Post";
                WarningMessage = L["SuccessDelete"];
                AlertVisibility = true;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message, $"{L["MSG_ApiError"]}");
                AlertTitle = $"{L["DeleteMsg"]} Post";
                WarningMessage = $"Erro ({ex.Message})";
                AlertVisibility = true;
            }
        }

        protected async Task HideToast()
        {
            await ToastObj!.HideAsync();
        }
    }
}