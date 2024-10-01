﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Uno.Extensions.DependencyInjection;
using Uno.UI.RemoteControl.Helpers;

namespace Uno.UI.RemoteControl.Host.Extensibility;

public static class AddInsExtensions
{
	public static IWebHostBuilder ConfigureAddIns(this IWebHostBuilder builder, string solutionFile)
	{
		AssemblyHelper.Load(AddIns.Discover(solutionFile), throwIfLoadFailed: true);

		return builder.ConfigureServices(svc => svc.AddFromAttribute());
	}
}
