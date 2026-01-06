param(
  [string]$Version
)

if (-not $Version) {
    $Version = Read-Host "Please enter the version number (e.g., 1.0.1)"
}

$image = "grga023/evidencijanezaposlenih_internetprogramiranje"

docker build -t "${image}:${Version}" -f Evidencijanezaposlenih.Interface/Dockerfile .
docker tag   "${image}:${Version}" "${image}:latest"
docker push  "${image}:${Version}"
docker push  "${image}:latest"
