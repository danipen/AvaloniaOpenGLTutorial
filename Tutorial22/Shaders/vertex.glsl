in vec3 position;
in vec2 texCoord;
in vec3 normal;

uniform mat4 gLocalTransform;
uniform mat4 gWorldTransform;

out vec2 texCoord0;
out vec3 normal0;

void main()
{
    gl_Position = gLocalTransform * vec4(position, 1.0);
    texCoord0 = texCoord;
    normal0 = (gWorldTransform * vec4(normal, 0.0)).xyz;
}