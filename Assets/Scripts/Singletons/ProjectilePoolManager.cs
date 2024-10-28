using System.Collections.Generic;
using UnityEngine;
using System;

public class ProjectilePoolManager : MonoBehaviour
{
    [Serializable]
    public class ProjectilePool
    {
        public GameObject prefab;
        public List<GameObject> projectiles = new List<GameObject>();

        public ProjectilePool(GameObject prefab)
        {
            this.prefab = prefab;
        }
    }
    [SerializeField] private Transform _transform;
    [SerializeField] private List<ProjectilePool> projectilePools = new List<ProjectilePool>(); //this and the class shouldn't be serializable i just like how it looks in the editor lol

    public static ProjectilePoolManager Instance { get; private set; }

    void Awake() 
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public GameObject GetProjectile(GameObject prefab)
    {
        ProjectilePool projectilePool = FindProjectilePool(prefab);

        if (projectilePool == null)
        {
            projectilePool = NewProjectilePool(prefab);
        }

        return GetFreeProjectile(projectilePool);
    }

    ProjectilePool FindProjectilePool(GameObject prefab)
    {
        for (int i = 0; i < projectilePools.Count; i++)
        {
            if (projectilePools[i].prefab == prefab)
            {
                return projectilePools[i];
            }
        }

        return null;
    }

    ProjectilePool NewProjectilePool(GameObject prefab)
    {
        projectilePools.Add(new ProjectilePool(prefab));
        return projectilePools[projectilePools.Count - 1];
    }

    GameObject GetFreeProjectile(ProjectilePool projectilePool)
    {
        for (int i = 0; i < projectilePool.projectiles.Count; i++)
        {
            if (projectilePool.projectiles[i].activeSelf == false)
            {
                projectilePool.projectiles[i].SetActive(true);
                return projectilePool.projectiles[i];
            }
        }

        projectilePool.projectiles.Add(Instantiate(projectilePool.prefab, _transform));
        return projectilePool.projectiles[projectilePool.projectiles.Count - 1];
    }

    public void ClearAll()
    {
        for (int i = 0; i < projectilePools.Count; i++)
        {
            for (int x = 0; x < projectilePools[i].projectiles.Count; x++)
            {
                Destroy(projectilePools[i].projectiles[x]);
            }
        }
    }
}