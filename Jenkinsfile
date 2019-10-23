node {
   stage "checkout"
   checkout scm
   stage 'compiling'
   bat label: '', script: 'dotnet restore'
   bat label: '', script: 'dotnet build -c Release'
   stage 'testing'
   bat label: '', script: 'dotnet test'
}
