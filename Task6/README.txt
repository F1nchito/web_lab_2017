��� ���������� scss ���������:
1. ���������� Node sass. "npm install -g node-sass less".
2. ������� � �������� ����� ������� ����� ".vscode".
3. � ��� ������� ���� tasks.json �� ��������� �����������:
   // Sass configuration
{
    "version": "0.1.0",
    "command": "node-sass",
    "isShellCommand": true,
    "args": ["scss/styles.scss", "css/styles.css"]
}
4. ��������� � ������������� VS CODE.
5. ������� ����� ������� � VS CODE, ������ CTRL+SHIFT+B.(��������� ����� ���� .css � ����� css/styles).
6. ������!