namespace GLDemo
{
    //Знаю, знаю...
    public static class HardcodedShaders
    {
        public const string BasicVertex =
            @"
#version 330 core
layout (location = 0) in vec3 position; // Устанавливаем позиция переменной с координатами в 0
layout (location = 1) in vec3 color;    // А позицию переменной с цветом в 1

out vec3 ourColor; // Передаем цвет во фрагментный шейдер

void main()
{
    gl_Position = vec4(position, 1.0);
    ourColor = color; // Устанавливаем значение цвета, полученное от вершинных данных
}   
";

        public const string BasicFragment =
            @"
#version 330 core
in vec3 ourColor;
out vec4 color;

void main()
{
    color = vec4(ourColor, 1.0f);
}
";
    }
}