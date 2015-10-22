
package entity;

import java.awt.image.BufferedImage;

public class Tank extends GameObject {

    private BufferedImage up;
    private BufferedImage right;
    private BufferedImage down;
    private BufferedImage left;
    private int health;
    private int points;
    private int direction;

    public Tank(int x, int y) {
        super(x, y);
    }

    public int getHealth() {
        return health;
    }

    public void setHealth(int health) {
        this.health = health;
    }

    public int getPoints() {
        return points;
    }

    public int getDirection() {
        return direction;
    }

    public void setDirection(int direction) {
        this.direction = direction;
    }
    
}
