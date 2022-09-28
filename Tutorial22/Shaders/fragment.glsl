in vec2 texCoord0;
in vec3 normal0;

out vec4 fragColor;

struct DirectionalLight
{
    vec3 Color;
    vec3 Direction;
    float AmbientIntensity;
    float DiffuseIntensity;
    float SpecularIntensity;
};

uniform DirectionalLight gDirectionalLight;
uniform vec3 gCameraDir;
uniform sampler2D gSampler;

void main()
{
    vec3 ambientColor = gDirectionalLight.Color * gDirectionalLight.AmbientIntensity;

    float diffuseFactor = dot(normalize(normal0), gDirectionalLight.Direction);

    vec3 diffuseColor = vec3(0,0,0);
    if (diffuseFactor > 0)
        diffuseColor = gDirectionalLight.Color *
            gDirectionalLight.DiffuseIntensity *
            diffuseFactor;

    vec3 toEye = normalize(vec3(0.0) - gCameraDir);
    vec3 lightRef = reflect(gDirectionalLight.Direction, normalize(normal0));
    float specularFactor = pow(dot(-lightRef, toEye), 64.0f);

    vec3 specularColor = vec3(0,0,0);
    if (specularFactor > 0)
        specularColor = gDirectionalLight.Color *
            gDirectionalLight.SpecularIntensity *
            specularFactor;

    fragColor = //texture(gSampler, texCoord0.xy) *
                vec4(ambientColor + diffuseColor + specularColor, 1);
}