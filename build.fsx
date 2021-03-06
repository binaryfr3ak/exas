#r @"packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.AssemblyInfoFile

RestorePackages()

let buildDir = "./build/"
let packDir = "./pack/"
let binDir = "./bin/"

let release = ReleaseNotesHelper.LoadReleaseNotes "RELEASE_NOTES.md"
let version = release.AssemblyVersion + "." + BuildServerHelper.buildVersion

let isLocalBuild = BuildServerHelper.buildServer = BuildServerHelper.BuildServer.LocalBuild

let productDesc = "see: https://github.com/mrpinkzh/exas"

Target "clean" (fun _ ->
   CleanDir buildDir
   CleanDir packDir
   CleanDir binDir
)

Target "trace-version" (fun _ ->
    trace version
)

Target "version" (fun _ ->
    CreateCSharpAssemblyInfo "./src/ExAs/Properties/Version.cs"
        [Attribute.Version version
         Attribute.FileVersion version
         Attribute.Description productDesc]
)

Target "compile-src" (fun _ ->
   !! "src/**/*.csproj"
      |> MSBuildRelease buildDir "Build"
      |> Log "compile-src output: "
)

Target "compile-test" (fun _ ->
   !! "tests/**/*.csproj"
      |> MSBuildRelease buildDir "Build"
      |> Log "compile-test output: "
)

Target "test" (fun _ ->
   !! (buildDir + "/*.Tests.dll")
      |> NUnit(fun p ->
         { p with
            ToolPath = "packages/NUnit.Runners/tools"
            OutputFile = buildDir + "/TestResults.xml" } 
         )
)

Target "appveyor-test-publish" (fun _ ->
    AppVeyor.UploadTestResultsXml AppVeyor.TestResultsType.NUnit "./build"
)

Target "pack-nuget" (fun _ ->
    CopyFiles packDir [buildDir + "ExAs.dll"]

    // on a local build the version would contain a "LocalBuild" string which nuget cannot handle
    let nugetVersion = if isLocalBuild
                       then release.AssemblyVersion
                       else version

    NuGet (fun p -> 
            { p with Version = nugetVersion
                     Description = productDesc
                     WorkingDir = packDir
                     AccessKey = getBuildParamOrDefault "nugetkey" ""
                     OutputPath = binDir
                     ReleaseNotes = toLines release.Notes
                     Files = [("ExAs.dll", Some "lib", None)] })
          "ExtendedAssertions.nuspec"
)

Target "publish" (fun _ ->
    Paket.Push(fun p ->
        { p with WorkingDir = binDir })
)

"clean"
  ==> "version"
  ==> "compile-src"
  ==> "compile-test"
  ==> "test"
  ==> "pack-nuget" 
  ==> "publish"

RunTargetOrDefault "pack-nuget"