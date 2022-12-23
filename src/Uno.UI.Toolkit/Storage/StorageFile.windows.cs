﻿#if WINDOWS_UWP || WINDOWS
#nullable enable

using System.Threading;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace Uno.UI.Toolkit;

partial class StorageFileHelper
{
	private static Task<bool> FileExistsInPackage(string fileName)
	{
		var executingPath = Assembly.GetExecutingAssembly().Location;
		if (!string.IsNullOrEmpty(executingPath))
		{
			var path = Path.GetDirectoryName(executingPath);
			if (!string.IsNullOrEmpty(path))
			{
				var fullPath = Path.Combine(path, fileName);
				return Task.FromResult(File.Exists(fullPath));
			}
		}

		return Task.FromResult(true);
	}
}
#endif
