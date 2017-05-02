Internal training 2017. Weblab.

## Task4

Before start a project, put next files to corresponding folders:

- `Task4`
  - `css`
     - `bootstrap-override.css`

Other css/js files will upload automatically from CDN.

## Task5

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

## Task7 

Task3 implemeted 3 languages(in file *l10n.js*) and days of week("ddd"-short name of day, "dddd"-full name of day).