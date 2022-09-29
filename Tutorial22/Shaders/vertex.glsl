in vec3 position;
in vec2 texCoord;
in vec3 normal;

uniform mat4 gLocalTransform;
uniform mat4 gWorldTransform;
uniform mat4 gMeshTransform;

out vec2 texCoord0;
out vec3 normal0;

void main()
{
    gl_Position = gLocalTransform * gMeshTransform * vec4(position, 1.0);
    texCoord0 = texCoord;
    mat3 norm_matrix = transpose(inverse(mat3(gWorldTransform * gMeshTransform)));
    normal0 = normalize(norm_matrix * normal);
}