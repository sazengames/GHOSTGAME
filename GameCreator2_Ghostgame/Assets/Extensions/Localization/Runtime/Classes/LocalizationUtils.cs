using System;
using System.Text.RegularExpressions;
using UnityEngine.Localization.Tables;

namespace GameCreator.Runtime.Localization
{
    public static class LocalizationUtils
    {
        private static readonly Regex RX_INFO = new Regex(@"^[^(]*\(");

        public static string ToString(TableReference tableRef, TableEntryReference entryRef)
        {
            string tableInfo = tableRef.ToString();
            string entryInfo = entryRef.ToString(tableRef);

            string[] tableData = RX_INFO
                .Replace(tableInfo, string.Empty)
                .Replace(")", string.Empty)
                .Split(" - ");
                
            string[] entryData = RX_INFO
                .Replace(entryInfo, string.Empty)
                .Replace(")", string.Empty)
                .Split(" - ");

            if (tableData.Length == 2 && entryData.Length == 2)
            {
                return $"{tableData[1]}[{entryData[1]}]";
            }
                
            string table = tableRef.ReferenceType switch
            {
                TableReference.Type.Empty => "(none)",
                TableReference.Type.Guid => tableRef.TableCollectionName,
                TableReference.Type.Name => tableRef.TableCollectionName,
                _ => throw new ArgumentOutOfRangeException()
            };
                
            string key = entryRef.ReferenceType switch
            {
                TableEntryReference.Type.Empty => "(none)",
                TableEntryReference.Type.Name => $"key:{entryRef.Key}",
                TableEntryReference.Type.Id => $"id:{entryRef.KeyId}",
                _ => throw new ArgumentOutOfRangeException()
            };

            return $"{table}[{key}]";
        }
    }
}