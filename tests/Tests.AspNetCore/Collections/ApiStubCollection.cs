using Fwks.Tests.AspNetCore.Fixtures;
using Fwks.Tests.Shared.Configuration;
using Xunit;

namespace Fwks.Tests.AspNetCore.Collections;

[CollectionDefinition(TestsConfiguration.COLLECTIONS_API_STUB)]
public class ApiStubCollection : ICollectionFixture<StubApiFixture> { }
