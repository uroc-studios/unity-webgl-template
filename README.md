# UROC Unity WebGL Template

## Fresh Install Instructions
Remove existing WebGL Template installation:
```shell
git rm -r --cached 'Assets/WebGLTemplates/UROC'
git rm --cached 'Assets/WebGLTemplates/UROC.meta'
```

Add this line to `.gitignore`
```gitignore
/[Aa]ssets/WebGLTemplates/UROC
/[Aa]ssets/WebGLTemplates/UROC.meta
```

Install this package in Package Manager by using the "Install package from git URL" option

```
https://git.internal.uroc.co/uroc-studios/unity-webgl-template.git
```