using FluentAssertions;
using NetArchTest.Rules;

namespace Architecture.Test;

public class ArchitectureTest
{
    private const string DomainNamespace = "Domain";
    private const string ApplicationNamespace = "Application";
    private const string InfrastructureNamespace = "Infrastructure";
    private const string PresentationNamespace = "Presentation";
    private const string WebApiNamespace = "WebApi";
    private const string GogglePlaceServiceNamespace = "GogglePlaceService";
    
    [Test]
    public void Domain_Should_Not_HaveDependencyOnOtherProject()
    {
        var assembly = typeof(Domain.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            PresentationNamespace,
            WebApiNamespace
        };

        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }
    
    [Test]
    public void Application_Should_Not_HaveDependencyOnOtherProject()
    {
        var assembly = typeof(Application.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            InfrastructureNamespace,
            PresentationNamespace,
            WebApiNamespace
        };

        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Test]
    public void Handlers_Should_Have_DependencyOnDomain()
    {
        var assembly = typeof(Application.AssemblyReference).Assembly;
        
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Handler")
            .Should()
            .HaveDependencyOn(DomainNamespace)
            .GetResult();
        
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Test]
    public void Infrastructure_Should_Not_HaveDependencyOnOtherProject()
    {
        var assembly = typeof(Infrastructure.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            ApplicationNamespace,
            PresentationNamespace,
            WebApiNamespace
        };

        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Test]
    public void Presentation_Should_Not_HaveDependencyOnOtherProject()
    {
        var assembly = typeof(Presentation.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            InfrastructureNamespace,
            WebApiNamespace
        };

        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Test]
    public void Controllers_Should_Have_DependencyOnMediatR()
    {
        
        var assembly = typeof(Presentation.AssemblyReference).Assembly;
        
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Controller")
            .Should()
            .HaveDependencyOn("MediatR")
            .GetResult();
        
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Test]
    public void No_Project_Should_Have_DependencyOnGoggleService()
    {
        var assembly = typeof(Application.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            InfrastructureNamespace,
            PresentationNamespace,
            WebApiNamespace,
            DomainNamespace,
            ApplicationNamespace
        };

        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOn(GogglePlaceServiceNamespace)
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }
}