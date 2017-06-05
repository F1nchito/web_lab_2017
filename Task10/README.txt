Для VSCODE
Для запуска в корневой папке проекта создать папку .vscode(если ее нет). В ней создать следующие файлы:
1. launch.json с следующим текстом : 
{
    "version": "0.2.0",
    "configurations": [
        {
            "type": "node",
            "request": "launch",
            "name": "Launch Program",
            "program": "${workspaceRoot}/server/server.js",
            "cwd": "${workspaceRoot}"
        },
        {
            "type": "node",
            "request": "attach",
            "name": "Attach to Process",
            "port": 3000
        }
    ]
}
Для запуска должен быть установлен nodeJS.
Серверная часть задания лежит в папке server, клиентская-site.
