
package tanksmash;

import entity.GameObject;
import java.awt.Canvas;
import java.awt.Color;
import java.awt.Graphics2D;
import java.awt.Toolkit;
import java.awt.image.BufferedImage;

public class MapDisplayUnit extends Canvas{

    private GameObject gameObject;
    
    public MapDisplayUnit() {
        this.setIgnoreRepaint(true);
    }

    public void draw() {
        Graphics2D g = (Graphics2D) this.getGraphics();
        BufferedImage bi = gameObject.getImage();
        if (bi==null){
            g.setColor(new Color(240,240,240));
            g.fillRect(0, 0, 50, 50);
        }else{
            g.drawImage(bi, 0, 0, this);
        }
        Toolkit.getDefaultToolkit().sync();
        g.dispose();
    }
    
    public GameObject getGameObject(){
        return gameObject;
    }
    
    public void setGameObject(GameObject gameObject){
        this.gameObject = gameObject;
    }
    
}
