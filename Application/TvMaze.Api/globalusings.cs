﻿global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading.Tasks;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using System.IO;
global using Microsoft.AspNetCore;
global using Microsoft.Extensions.Logging;
global using System.Security.Claims;
global using MediatR;
global using TvMaze.Core.Messages.DomainMessages;
global using TvMaze.Application;
global using TvMaze.Application.ViewModels;
global using Microsoft.AspNetCore.Authorization;
global using TvMaze.IoC;