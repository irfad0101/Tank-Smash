
package entity;

import java.awt.image.BufferedImage;

public class Bullet extends GameObject {
    
    private static BufferedImage bullet;
    private int directoin;

    public Bullet(int x, int y) {
        super(x, y);
    }
    
}
