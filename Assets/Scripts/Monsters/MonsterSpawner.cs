using UnityEngine;
using System.Collections;

public class MonsterSpawner : MonoBehaviour {

	private float nextLionSpawn = 0.0f;
	private float lionSpawnRate = 4.0f;
	private float nextLizSpawn = 0.0f;
	private float lizSpawnRate = 6.0f;

	private int maxLions = 4;
	private int maxLizards = 4;
	
	private FlyingLion lionPrefab;
	private LizardMonster lizPrefab;

	// Use this for initialization
	void Start () {
		lionPrefab = Resources.Load<FlyingLion>("FlyingLion");
		lizPrefab = Resources.Load<LizardMonster>("Lizard_monster");
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= nextLionSpawn && FlyingLion.GetNumberOf() < maxLions){
			FlyingLion lion = Instantiate (lionPrefab) as FlyingLion;
		
			lion.gameObject.transform.position = RandomMonsterPosition();
			nextLionSpawn = Time.time + lionSpawnRate;

			if (Time.time > 180.0f) {
				lion.GetComponent<FlyingLion>()._approachSpeed *= 1.5f;
			}
			if (Time.time > 360.0f) {
				lion.GetComponent<FlyingLion>()._approachSpeed *= 1.5f;
			}
			
		}
	
		if (Time.time >= nextLizSpawn && LizardMonster.GetNumberOf() < maxLizards){
			LizardMonster liz = Instantiate (lizPrefab) as LizardMonster;
			liz.transform.position = RandomMonsterPosition();
			nextLizSpawn = Time.time + lizSpawnRate;
			
			if (Time.time > 240.0f) {
				liz.xSpeed *= 2.0f;
			}
		}
	}

	Vector3 RandomMonsterPosition(){
		float x = Ship.instance.transform.position.x;
		if ( Util.RandomBool() )
			x += 15.0f;
		else
			x -= 15.0f;
		
		float y = Random.Range (5.0f, 9.0f);
		return new Vector3(x, y, 0.0f);
	}
}
