using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Bam.Net.Data.Schema
{
    public interface ITargetStreamResolver
    {
        Stream GetTargetContextStream(Func<string, Stream> targetResolver, string root, SchemaDefinition schema);
        Stream GetTargetClassStream(Func<string, Stream> targetResolver, string root, Table table);
        Stream GetTargetQueryClassStream(Func<string, Stream> targetResolver, string root, Table table);
        Stream GetTargetPagedQueryClassStream(Func<string, Stream> targetResolver, string root, Table table);
        Stream GetTargetQiClassStream(Func<string, Stream> targetResolver, string root, Table table);
        Stream GetTargetCollectionStream(Func<string, Stream> targetResolver, string root, Table table);
        Stream GetTargetColumnsClassStream(Func<string, Stream> targetResolver, string root, Table table);
    }
}
