in vec2 texCoord0;
in vec3 normal0;

out vec4 fragColor;

struct DirectionalLight
{
    vec3 Color;
    vec3 Direction;
    float Intensity;
};

struct Material
{
    vec3 AmbientColor;
    vec3 DiffuseColor;
    vec3 SpecularColor;
    float Shininess;
    float ShininessStrength;
};

uniform DirectionalLight gDirectionalLight;
uniform Material gMaterial;
uniform vec3 gCameraDir;
uniform sampler2D gSampler;

void main()
{
    float diffuseFactor = dot(normal0, gDirectionalLight.Direction);

    vec3 diffuseColor = vec3(0,0,0);
    if (diffuseFactor > 0)
        diffuseColor = gMaterial.DiffuseColor * diffuseFactor;

    vec3 specularColor = vec3(0,0,0);
    if (gMaterial.Shininess > 0)
    {
        vec3 toEye = normalize(vec3(0.0) - gCameraDir);
        vec3 lightRef = reflect(gDirectionalLight.Direction, normal0);
        float specularFactor = dot(-lightRef, toEye);

        specularColor = gMaterial.SpecularColor * gMaterial.ShininessStrength * pow(specularFactor, gMaterial.Shininess);
    }

    vec3 materialColor = gMaterial.AmbientColor + diffuseColor + specularColor;
    vec3 globalLighting = (gMaterial.AmbientColor + gMaterial.DiffuseColor + gMaterial.SpecularColor) * gDirectionalLight.Intensity;

    fragColor = texture(gSampler, texCoord0.xy) *
                vec4(gDirectionalLight.Color, 1) *
                vec4(materialColor + globalLighting, 1);
}