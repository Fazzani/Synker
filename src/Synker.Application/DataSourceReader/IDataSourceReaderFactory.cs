using Synker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Synker.Application.Interfaces
{
    public interface IDataSourceReaderFactory
    {
        IDataSourceReader Create(PlaylistDataSource playlistDataSource);
    }
}
