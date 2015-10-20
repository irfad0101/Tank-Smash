
package tanksmash;

import java.awt.image.BufferedImage;

/*
 * @author Irfad Hussain
 */
public class Bullet extends GameObject {
    
    private static BufferedImage bullet;
    private int directoin;

    public Bullet(int x, int y) {
        super(x, y);
    }
    
}
