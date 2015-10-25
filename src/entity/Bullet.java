
package entity;

import java.awt.image.BufferedImage;
import java.io.IOException;
import javax.imageio.ImageIO;

public class Bullet extends GameObject implements Runnable {
    
    private static BufferedImage bullet0 = null;
    private static BufferedImage bullet1 = null;
    private static BufferedImage bullet2 = null;
    private static BufferedImage bullet3 = null;
    
    private int directoin;

    public Bullet(int x, int y,int direction) throws IOException {
        super(x, y);
        this.directoin = direction;
        // load images
        if (bullet0==null){
            bullet0 = ImageIO.read(getClass().getResourceAsStream("/resources/bullet0.jpg"));
        }
        if (bullet1==null){
            bullet1 = ImageIO.read(getClass().getResourceAsStream("/resources/bullet1.jpg"));
        }
        if (bullet2==null){
            bullet2 = ImageIO.read(getClass().getResourceAsStream("/resources/bullet2.jpg"));
        }
        if (bullet3==null){
            bullet3 = ImageIO.read(getClass().getResourceAsStream("/resources/bullet3.jpg"));
        }
    }
    
    @Override
    public BufferedImage getImage() {
        // return image to be drawed according to directon of bullet
        switch(directoin){
            case 0:
                return bullet0;
            case 1:
                return bullet1;
            case 2:
                return bullet2;
            case 3:
                return bullet3;
            default:
                return null;
        }
    }

    @Override
    public void run() {
        
    }
    
}
