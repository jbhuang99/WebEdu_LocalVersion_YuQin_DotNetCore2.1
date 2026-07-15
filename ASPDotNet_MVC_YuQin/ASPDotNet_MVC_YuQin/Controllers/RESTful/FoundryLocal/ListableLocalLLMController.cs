using ASPDotNet_MVC_YuQin.FoundryLocalDemo;
using Azure;
using DocumentFormat.OpenXml.Wordprocessing;
using FoundryLocalDemo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASPDotNet_MVC_YuQin.Controllers.FoundryLocalDemo
{
    //[Authorize()]
    public class ListableLocalLLMController : ControllerBase
    {
        
        private ObservableCollection<ModelViewModel> _availableModels = new();
        private ObservableCollection<ModelViewModel> _downloadedModels = new();
        private ObservableCollection<ModelViewModel> _availableForDownloadModels = new();
        private CancellationTokenSource? _currentCancellationTokenSource;
        private string? _selectedModelName;
        
        private bool _isProcessingProfile;

        private readonly IWebHostEnvironment _iWebHostEnvironment;
        private readonly IConfiguration _iConfiguration;
       
        public ListableLocalLLMController(IWebHostEnvironment iWebHostEnvironment, IConfiguration iConfiguration)
        {
            _iWebHostEnvironment = iWebHostEnvironment;
            _iConfiguration = iConfiguration;
            if (iWebHostEnvironment.EnvironmentName=="Development") {
                //_ApiKey = ConfigurationManager.AppSettings["ApiKey"];
                //_ApiKey = _iConfiguration["ApiKey"]; //从开发时的本项目的“Secret Manager”的secrets.json文件获取ApiKey。
               // Console.Write(iWebHostEnvironment.EnvironmentName+ _ApiKey);
            }
            else{
                //_ApiKey = ConfigurationManager.AppSettings["ApiKey"];
               // _ApiKey = _iConfiguration["ApiKey"]; //从交付时的软件的appsettings.json文件获取ApiKey。
              //  Console.Write(iWebHostEnvironment.EnvironmentName + _ApiKey);
            }
        }
        public async Task<string> Index()
        {
            var catalogModels = await ExecutionLocalLLM.ListCatalogModelsAsync();
            var cachedModels = await ExecutionLocalLLM.ListCachedModelsAsync();
            var cachedModelNames = cachedModels.Select(m => m.ModelId).ToHashSet();
            String TempString = "";
            try
            {
                // Clear existing models
                //AvailableModels.Clear();
               // DownloadedModels.Clear();
                //AvailableForDownloadModels.Clear();

                // Load catalog models
               

                foreach (var model in catalogModels)
                {
                    var modelViewModel = new ModelViewModel
                    {
                        Name = model.ModelId,
                        DeviceType = model.Runtime.DeviceType.ToString(),
                        IsDownloaded = cachedModelNames.Contains(model.ModelId),
                        IsDownloading = false,
                        IsLoaded = false, // Models need to be loaded into memory after download
                        IsLoading = false,
                        DownloadProgress = 0,
                        DownloadStatus = ""
                    };

                    // Add to main collection
                    AvailableModels.Add(modelViewModel);

                    // Add to appropriate separated collection
                    if (modelViewModel.IsDownloaded)
                    {
                        DownloadedModels.Add(modelViewModel);
                    }
                    else
                    {
                        AvailableForDownloadModels.Add(modelViewModel);
                    }
                }
            }
            catch (Exception ex)
            {
               // StatusText.Text = $"Error loading models: {ex.Message}";
            }
            
           
            foreach (var catalogModel in catalogModels)
            {
                var modelViewModel = new ModelViewModel
                {
                    Name = catalogModel.ModelId,
                    DeviceType = catalogModel.Runtime.DeviceType.ToString(),
                    IsDownloaded = cachedModelNames.Contains(catalogModel.ModelId),
                    IsDownloading = false,
                    IsLoaded = false, // Models need to be loaded into memory after download
                    IsLoading = false,
                    DownloadProgress = 0,
                    DownloadStatus = "",
                    DownLoadSize= catalogModel.FileSizeMb,
                    License = catalogModel.License
                };
                TempString = TempString + modelViewModel.Name+ "（文件容量：" + modelViewModel.DownLoadSize + "M；开源许可：" + modelViewModel.License +"）"+ "|||";

                // Add to main collection
                AvailableModels.Add(modelViewModel);

                // Add to appropriate separated collection
                if (modelViewModel.IsDownloaded)
                {
                    DownloadedModels.Add(modelViewModel);
                }
                else
                {
                    AvailableForDownloadModels.Add(modelViewModel);
                }
            }
            Console.WriteLine(TempString);
            return TempString;
            
        }

        public ObservableCollection<ModelViewModel> AvailableModels
        {
            get => _availableModels;
            set
            {
                _availableModels = value;
                //OnPropertyChanged();
            }
        }

        public ObservableCollection<ModelViewModel> DownloadedModels
        {
            get => _downloadedModels;
            set
            {
                _downloadedModels = value;
                //OnPropertyChanged();
            }
        }

        public ObservableCollection<ModelViewModel> AvailableForDownloadModels
        {
            get => _availableForDownloadModels;
            set
            {
                _availableForDownloadModels = value;
                //OnPropertyChanged();
            }
        }

        public string? SelectedModelName
        {
            get => _selectedModelName;
            set
            {
                if (_selectedModelName != value)
                {
                    _selectedModelName = value;
                    // OnPropertyChanged();
                    //UpdateSelectedModelText();

                    // Auto-process selected message if model is ready and message is selected
                    //if (!string.IsNullOrEmpty(value) && _selectedMessage != null)
                    if (!string.IsNullOrEmpty(value))
                    {
                        var selectedModel = AvailableModels.FirstOrDefault(m => m.Name == value);
                        if (selectedModel?.IsLoaded == true)
                        {
                            //_ = ProcessSelectedMessage();
                        }
                    }
                }
            }
        }
    }
}
