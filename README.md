目的：使用projector作为场景视频融合的投影工具，将监控画面投影到模型上。

在 Unity HDRP 2019.4 中测试成功。

shadergraph文件可直接导入Unity 2021使用，2019需自行修改（抄）。

![image](https://github.com/paploli/Unity-HDRP-Projector-CustomPass/blob/main/SceneView.png?raw=true)

* 修改投影图像方向

![image](https://github.com/paploli/Unity-HDRP-Projector-CustomPass/blob/main/Script.png?raw=true)

* 为了投影图像清晰，所以取消衰减效果，改为裁切

![image](https://github.com/paploli/Unity-HDRP-Projector-CustomPass/blob/main/ShaderGraph.png?raw=true)

* 同时，为了不被现有地面/墙壁材质影响，需要修改材质上的BlendingMode，Material / Blending Mode / Additive -> Alpha

![image](https://github.com/paploli/Unity-HDRP-Projector-CustomPass/blob/main/Material.png?raw=true)





# HDRP_Projector_CustomPass
 ✨Unity HDRP Projector in Custom Pass
 
![image](https://github.com/FunsTW/HDRP_Projector_CustomPass/blob/main/SceneVeiw.jpg?raw=true)

![image](https://github.com/FunsTW/HDRP_Projector_CustomPass/blob/main/ShaderGraph.png?raw=true)

## System requirements
* Unity HDRP 2021.2 or above *( Maybe just need to have "custom pass volume (script)" to execute it. )(?)*

![image](https://github.com/FunsTW/HDRP_Projector_CustomPass/blob/main/ProjectorScript.jpg?raw=true)

![image](https://github.com/FunsTW/HDRP_Projector_CustomPass/blob/main/CustomPass.jpg?raw=true)
