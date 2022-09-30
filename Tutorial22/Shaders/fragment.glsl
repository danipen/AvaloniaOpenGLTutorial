in vec2 texCoord0;
in vec3 normal0;

out vec4 fragColor;

struct DirectionalLight
{
    vec3 Color;
    vec3 Direction;
    float GlobalIntensity;
    float AmbientIntensity;
    float SpecularIntensity;
    float DiffuseIntensity;
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

vec3 calculateSpecularLighting()
{
    vec3 R = reflect(gDirectionalLight.Direction, normal0);
    float specularFactor = max(dot(gCameraDir, R), 0);
    return gMaterial.SpecularColor * gMaterial.ShininessStrength * pow(specularFactor, gMaterial.Shininess);
}

vec3 calculateDiffuseLighting()
{
    float diffuseFactor = max(dot(normal0, gDirectionalLight.Direction),0);
    return gMaterial.DiffuseColor * diffuseFactor;
}

vec3 calculateMaterialLighting()
{
    vec3 diffuseLighting = calculateDiffuseLighting();
    vec3 specularLighting = calculateSpecularLighting();

    return  gMaterial.AmbientColor * gDirectionalLight.AmbientIntensity +
            diffuseLighting * gDirectionalLight.DiffuseIntensity +
            specularLighting * gDirectionalLight.SpecularIntensity;
}

vec3 calculateGlobalLighting()
{
    return (gMaterial.AmbientColor + gMaterial.DiffuseColor + gMaterial.SpecularColor) *
            gDirectionalLight.GlobalIntensity;
}

void main()
{
    vec3 materialLighting = calculateMaterialLighting();
    vec3 globalLighting = calculateGlobalLighting();

    fragColor = //texture(gSampler, texCoord0.xy) *
                vec4(gDirectionalLight.Color, 1) *
                vec4(materialLighting + globalLighting, 1);
}