using UnityEngine;
using UnityEditor;
using System.Runtime.InteropServices;
using UnityEngine.Events;

internal sealed class AudioImportSettingsEditor : AssetPostprocessor
{
    private void OnPreprocessAudio()
    {
        var importer = assetImporter as AudioImporter;

        if (importer == null)
        {
            return;
        }
        
        AudioImporterSampleSettings sampleSettings = importer.defaultSampleSettings;
        sampleSettings.loadType = AudioClipLoadType.Streaming;
        sampleSettings.compressionFormat = AudioCompressionFormat.Vorbis;
        sampleSettings.quality = 1;
        importer.defaultSampleSettings = sampleSettings;
    }
}