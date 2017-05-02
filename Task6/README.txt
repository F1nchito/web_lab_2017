Для компиляции scss требуется:
1. Установить Node sass. "npm install -g node-sass less".
2. Создать в корневой папке задания папку ".vscode".
3. В ней создать файл tasks.json со следующим содержанием:
   // Sass configuration
{
    "version": "0.1.0",
    "command": "node-sass",
    "isShellCommand": true,
    "args": ["scss/styles.scss", "css/styles.css"]
}
4. Сохранить и перезагрузить VS CODE.
5. Открыть папку проекта в VS CODE, нажать CTRL+SHIFT+B.(создастся новый файл .css в папке css/styles).
6. Готово!