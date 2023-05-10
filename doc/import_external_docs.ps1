Set-PSDebug -Trace 1

<<<<<<< HEAD
$external_docs =
@(
    @("https://github.com/unoplatform/uno.wasm.bootstrap", "uno.wasm.bootstrap", "4abadfc93ffeddc82420cc28af04cd7f6b2693ab"),
    @("https://github.com/unoplatform/uno.themes", "uno.themes", "36020096cbed81be67385d25feaa711620f42778"),
    @("https://github.com/unoplatform/uno.toolkit.ui", "uno.toolkit.ui", "e6bd3390e8b21ab649294ee591d7f134f8d56197"),
    @("https://github.com/unoplatform/uno.check", "uno.check", "5dec33b3cb4c26f578c8d6bd7a84000bf265a14e"),
    @("https://github.com/unoplatform/uno.xamlmerge.task", "uno.xamlmerge.task", "7e8ffef206e87dfea90c53805c45e93a7d8c0b46"),
    @("https://github.com/unoplatform/figma-docs", "figma-docs", "f13d08f2bd7b62fc274b43a4ede4d75909d0f41f"),
    @("https://github.com/unoplatform/uno.resizetizer", "uno.resizetizer", "e23dfa53da77e48510173e99a29c260b08b826f5"),
    @("https://github.com/unoplatform/uno.uitest", "uno.uitest", "555453c2985ef2745fe44503c5809a6168d063c2"),
    @("https://github.com/unoplatform/uno.extensions", "uno.extensions", "767fe7eeb610eb2acadcd2e0f79bc55c43f59f70")
)
=======
$external_docs = @{
    # use either commit, or branch name to use its latest commit
    "uno.wasm.bootstrap" = "4abadfc93ffeddc82420cc28af04cd7f6b2693ab"
    "uno.themes"         = "3d12f341f3ce9ecd7738e163a3a0904e9b94466f"
    "uno.toolkit.ui"     = "434712b657f479d1329ff60af3b6f22bb6fdb34c"
    "uno.check"          = "5dec33b3cb4c26f578c8d6bd7a84000bf265a14e"
    "uno.xamlmerge.task" = "7e8ffef206e87dfea90c53805c45e93a7d8c0b46"
    "figma-docs"         = "f13d08f2bd7b62fc274b43a4ede4d75909d0f41f"
    "uno.resizetizer"    = "6ebb69b1e9d442b2304e9e4d41274bf46c00de87"
    "uno.uitest"         = "555453c2985ef2745fe44503c5809a6168d063c2"
    "uno.extensions"     = "539d6b0f2e61fbc2ae5d6e35a77de41cafacf5ce"      
}

$uno_git_url = "https://github.com/unoplatform/"

if($branches -ne $null)
{
    foreach ($repo in $branches.keys)
    {
        $branch = $branches[$repo]

        $external_docs[$repo] = $branch
    }
}

echo "Current setup:"
$external_docs
>>>>>>> 20a4b680d1 (chore: updated uno.resizetizer commit)

$ErrorActionPreference = 'Stop'

function Assert-ExitCodeIsZero()
{
    if ($LASTEXITCODE -ne 0)
    {
        throw "Exit code must be zero."
    }
}

mkdir articles\external -ErrorAction Continue
pushd articles\external

# ensure long paths are supported on Windows
git config --global core.longpaths true

# Heads - Release
for ($i = 0; $i -lt $external_docs.Length; $i++)
{
    $repoUrl=$external_docs[$i][0]
    $repoPath=$external_docs[$i][1]
    $repoBranch=$external_docs[$i][2]

    echo "Cloning $repoPath ($repoUrl@$repoBranch)"

    git clone $repoUrl $repoPath
    Assert-ExitCodeIsZero

    pushd $repoPath
    git checkout $repoBranch
    Assert-ExitCodeIsZero
    popd
}

popd
