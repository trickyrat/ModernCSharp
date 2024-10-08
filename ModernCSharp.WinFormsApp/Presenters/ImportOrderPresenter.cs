﻿using ModernCSharp.Application.Models;
using ModernCSharp.Application.Services.FileImportServices;
using ModernCSharp.Application.Utils;
using ModernCSharp.WinFormsApp.Common;
using ModernCSharp.WinFormsApp.Views;

namespace ModernCSharp.WinFormsApp.Presenters;

public class ImportOrderPresenter : AbstractPresenter<ImportOrdersView, ImportOrderPresenter>
{
    private readonly FileImporterFactory _importService;

    public ImportOrderPresenter(FileImporterFactory importService)
    {
        _importService = importService;
    }

    public async Task<IResult> Import(string filePath)
    {
        var extension = Path.GetExtension(filePath);
        var fileType = FileIOUtils.MapToFileType(extension);
        var importContext = new FileImportContext(fileType, filePath);
        return await _importService.ImportAsync(importContext);
    }
}
