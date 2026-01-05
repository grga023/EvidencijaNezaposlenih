param(
  [string]$Version = "1.0.0"
)

$image = "grga023/evidencijanezaposlenih_internetprogramiranje"

docker build -t "${image}:${Version}" -f Evidencijanezaposlenih.Interface/Dockerfile .
docker tag   "${image}:${Version}" "${image}:latest"
docker push  "${image}:${Version}"
docker push  "${image}:latest"
